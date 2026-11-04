import { execFileSync } from "node:child_process";
import { appendFileSync } from "node:fs";
import { pathToFileURL } from "node:url";

const BUMPS_FROM_LOWEST = ["patch", "minor", "major"];
const DEFAULT_BUMP = "patch";
const SEMVER_LABEL_PREFIX = "semver:";
const VERSION_TAG = /^(\d+)\.(\d+)\.(\d+)$/;

function parseVersion(tag) {
    const match = VERSION_TAG.exec(tag);
    return match ? match.slice(1).map(Number) : null;
}

function compareVersions(left, right) {
    return left[0] - right[0] || left[1] - right[1] || left[2] - right[2];
}

function highestBump(bumps) {
    return bumps.reduce((highest, bump) =>
        BUMPS_FROM_LOWEST.indexOf(bump) > BUMPS_FROM_LOWEST.indexOf(highest) ? bump : highest);
}

export function bumpForPr(labelNames) {
    const bumps = labelNames
        .filter(name => name.startsWith(SEMVER_LABEL_PREFIX))
        .map(name => name.slice(SEMVER_LABEL_PREFIX.length))
        .filter(bump => BUMPS_FROM_LOWEST.includes(bump));

    return bumps.length > 0 ? highestBump(bumps) : DEFAULT_BUMP;
}

export function latestVersionTag(tags) {
    const versions = tags.filter(tag => parseVersion(tag));

    if (versions.length === 0) {
        throw new Error("There is no X.Y.Z tag to version from.");
    }

    return versions.reduce((latest, tag) =>
        compareVersions(parseVersion(tag), parseVersion(latest)) > 0 ? tag : latest);
}

export function nextVersion(latestTag, prBumps) {
    if (prBumps.length === 0) {
        throw new Error(`There is nothing to release: no commits since ${latestTag}.`);
    }

    const [major, minor, patch] = parseVersion(latestTag);

    switch (highestBump(prBumps)) {
        case "major": return `${major + 1}.0.0`;
        case "minor": return `${major}.${minor + 1}.0`;
        default: return `${major}.${minor}.${patch + 1}`;
    }
}

export function ensureTagIsNew(version, tags) {
    if (tags.includes(version)) {
        throw new Error(`Tag ${version} already exists. Re-run only the failed release job, or check why the last release wasn't tagged.`);
    }
}

function run(command, args) {
    return execFileSync(command, args, { encoding: "utf8" }).trim();
}

function lines(output) {
    return output ? output.split("\n") : [];
}

function prLabelsForCommit(repository, sha) {
    const pulls = JSON.parse(run("gh", ["api", `repos/${repository}/commits/${sha}/pulls`]));
    return pulls.length > 0 ? pulls[0].labels.map(label => label.name) : [];
}

function main() {
    const { GITHUB_REPOSITORY, GITHUB_OUTPUT } = process.env;

    const tags = lines(run("git", ["tag", "--list"]));
    const latestTag = latestVersionTag(tags);
    const commits = lines(run("git", ["rev-list", `${latestTag}..HEAD`]));
    const prBumps = commits.map(sha => bumpForPr(prLabelsForCommit(GITHUB_REPOSITORY, sha)));

    const version = nextVersion(latestTag, prBumps);
    ensureTagIsNew(version, tags);

    console.log(`${commits.length} commit(s) since ${latestTag} (highest bump: ${highestBump(prBumps)}). Next version: ${version}.`);
    appendFileSync(GITHUB_OUTPUT, `version=${version}\n`);
}

const runAsScript = import.meta.url === pathToFileURL(process.argv[1]).href;

if (runAsScript) {
    try {
        main();
    } catch (error) {
        console.error(`::error::${error.message}`);
        process.exitCode = 1;
    }
}
