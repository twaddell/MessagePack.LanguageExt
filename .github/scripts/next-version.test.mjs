import { test } from "node:test";
import assert from "node:assert/strict";
import { bumpForPr, ensureTagIsNew, latestVersionTag, nextVersion } from "./next-version.mjs";

test("a patch bumps the patch number", () => {
    assert.equal(nextVersion("2.0.1", ["patch"]), "2.0.2");
});

test("the highest bump across PRs wins, and lower numbers reset", () => {
    assert.equal(nextVersion("2.0.1", ["patch", "minor"]), "2.1.0");
    assert.equal(nextVersion("2.0.1", ["minor", "major"]), "3.0.0");
    assert.equal(nextVersion("2.3.4", ["patch", "minor", "patch"]), "2.4.0");
});

test("no PRs since the last tag means nothing to release", () => {
    assert.throws(() => nextVersion("2.0.1", []), /nothing to release/);
});

test("a PR's bump comes from its semver label, ignoring other labels", () => {
    assert.equal(bumpForPr(["bug", "semver:minor"]), "minor");
    assert.equal(bumpForPr(["semver:major", "dependencies"]), "major");
});

test("a PR with no semver label counts as a patch", () => {
    assert.equal(bumpForPr([]), "patch");
    assert.equal(bumpForPr(["dependencies"]), "patch");
});

test("a PR with several semver labels takes the highest", () => {
    assert.equal(bumpForPr(["semver:patch", "semver:major"]), "major");
});

test("the latest tag is the highest X.Y.Z, not the most recent", () => {
    assert.equal(latestVersionTag(["2.0.1", "1.2.4", "2.0.0"]), "2.0.1");
    assert.equal(latestVersionTag(["2.0.9", "2.0.10"]), "2.0.10");
});

test("tags that aren't X.Y.Z are ignored", () => {
    assert.equal(latestVersionTag(["v9.0.0", "3.0.0-beta.1", "release-4", "2.0.1"]), "2.0.1");
});

test("no X.Y.Z tag at all is an error", () => {
    assert.throws(() => latestVersionTag(["v1.0.0"]), /no X\.Y\.Z tag/);
});

test("a version whose tag already exists is rejected", () => {
    assert.throws(() => ensureTagIsNew("2.0.2", ["2.0.1", "2.0.2"]), /2\.0\.2 already exists/);
    assert.doesNotThrow(() => ensureTagIsNew("2.0.3", ["2.0.1", "2.0.2"]));
});
