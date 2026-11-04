import { test } from "node:test";
import assert from "node:assert/strict";
import { validateSemverLabels } from "./semver-label-check.mjs";

test("a PR with no labels fails", () => {
    const result = validateSemverLabels([]);

    assert.equal(result.ok, false);
    assert.match(result.error, /add exactly one of semver:patch, semver:minor, semver:major/);
});

test("a PR with one semver label passes", () => {
    for (const label of ["semver:patch", "semver:minor", "semver:major"]) {
        assert.deepEqual(validateSemverLabels([label]), { ok: true });
    }
});

test("a PR with two semver labels fails naming both", () => {
    const result = validateSemverLabels(["semver:minor", "semver:major"]);

    assert.equal(result.ok, false);
    assert.match(result.error, /semver:minor/);
    assert.match(result.error, /semver:major/);
});

test("unrelated labels are ignored alongside one semver label", () => {
    assert.deepEqual(validateSemverLabels(["dependencies", "semver:patch", "bug"]), { ok: true });
});

test("only unrelated labels fails", () => {
    assert.equal(validateSemverLabels(["dependencies", "bug"]).ok, false);
});

test("labels are matched exactly and case-sensitively", () => {
    assert.equal(validateSemverLabels(["Semver:Minor"]).ok, false);
    assert.equal(validateSemverLabels(["semver:minor "]).ok, false);
});
