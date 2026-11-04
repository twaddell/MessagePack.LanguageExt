import { pathToFileURL } from "node:url";

export const SEMVER_LABELS = ["semver:patch", "semver:minor", "semver:major"];

const missingLabelError = `This PR needs a version bump label: add exactly one of ${SEMVER_LABELS.join(", ")}.`;

export function validateSemverLabels(labelNames) {
    const semverLabels = labelNames.filter(name => SEMVER_LABELS.includes(name));

    if (semverLabels.length === 0) {
        return { ok: false, error: missingLabelError };
    }

    if (semverLabels.length > 1) {
        return { ok: false, error: `This PR has more than one version bump label (${semverLabels.join(", ")}). Keep exactly one.` };
    }

    return { ok: true };
}

function main() {
    const result = validateSemverLabels(JSON.parse(process.env.PR_LABELS ?? "[]"));

    if (!result.ok) {
        console.error(`::error::${result.error}`);
        process.exitCode = 1;
    }
}

const runAsScript = import.meta.url === pathToFileURL(process.argv[1]).href;

if (runAsScript) {
    main();
}
