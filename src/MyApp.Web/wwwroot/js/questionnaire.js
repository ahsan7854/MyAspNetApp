document.addEventListener("click", (event) => {
    const target = event.target;
    if (!(target instanceof HTMLElement)) {
        return;
    }

    const addButton = target.closest(".repeat-record");
    if (addButton instanceof HTMLButtonElement) {
        const section = addButton.closest(".questionnaire-section");
        if (!(section instanceof HTMLElement)) {
            return;
        }

        const sectionKey = section.dataset.sectionKey;
        const matchingSections = Array.from(
            document.querySelectorAll(`.questionnaire-section[data-section-key="${sectionKey}"]`));
        const recordNumbers = matchingSections.map((matchingSection) => {
            const match = matchingSection.id.match(/record-(\d+)$/);
            return match ? Number(match[1]) : 1;
        });
        const recordNumber = Math.max(...recordNumbers) + 1;
        if (recordNumber > 20) {
            return;
        }

        const clone = section.cloneNode(true);
        if (!(clone instanceof HTMLElement)) {
            return;
        }

        clone.id = `section-${sectionKey}-record-${recordNumber}`;
        const heading = clone.querySelector("h2");
        if (heading) {
            heading.id = `section-heading-${sectionKey}-record-${recordNumber}`;
            heading.textContent = `${sectionKey}. ${section.dataset.sectionTitle} (additional record ${recordNumber})`;
            clone.setAttribute("aria-labelledby", heading.id);
        }

        clone.querySelectorAll("input, select, textarea").forEach((control) => {
            if (control instanceof HTMLInputElement || control instanceof HTMLSelectElement || control instanceof HTMLTextAreaElement) {
                control.value = "";
                control.removeAttribute("required");
                const answerName = control.name.match(/^Answers\[([A-Za-z][A-Za-z0-9]*)(?:__\d+)?\]$/);
                if (answerName) {
                    control.name = `Answers[${answerName[1]}__${recordNumber}]`;
                }
                if (control.id) {
                    control.id = control.id.replace(/__\d+$/, "") + `__${recordNumber}`;
                }
            }
        });

        clone.querySelectorAll("label[for]").forEach((label) => {
            const currentFor = label.getAttribute("for");
            if (currentFor) {
                label.setAttribute("for", currentFor.replace(/__\d+$/, "") + `__${recordNumber}`);
            }
        });

        const buttonGroup = clone.querySelector(".questionnaire-section-heading");
        if (buttonGroup) {
            const removeButton = document.createElement("button");
            removeButton.type = "button";
            removeButton.className = "button button-secondary remove-record";
            removeButton.textContent = "Remove this record";
            removeButton.setAttribute("aria-label", `Remove additional record ${recordNumber}`);
            buttonGroup.insertBefore(removeButton, buttonGroup.querySelector(".repeat-record"));
        }

        const lastSection = matchingSections[matchingSections.length - 1];
        lastSection.insertAdjacentElement("afterend", clone);
        const repeatButtons = document.querySelectorAll(
            `.questionnaire-section[data-section-key="${sectionKey}"] .repeat-record`);
        if (recordNumber === 20) {
            repeatButtons.forEach((button) => {
                button.disabled = true;
            });
        }
        return;
    }

    const removeButton = target.closest(".remove-record");
    if (removeButton instanceof HTMLButtonElement) {
        const section = removeButton.closest(".questionnaire-section");
        const sectionKey = section?.dataset.sectionKey;
        section?.remove();
        if (sectionKey) {
            const remainingSections = document.querySelectorAll(
                `.questionnaire-section[data-section-key="${sectionKey}"]`);
            const highestRecordNumber = Array.from(remainingSections).reduce((highest, remainingSection) => {
                const match = remainingSection.id.match(/record-(\d+)$/);
                return Math.max(highest, match ? Number(match[1]) : 1);
            }, 1);
            if (highestRecordNumber < 20) {
                remainingSections.forEach((remainingSection) => {
                    remainingSection.querySelector(".repeat-record")?.removeAttribute("disabled");
                });
            }
        }
    }
});
