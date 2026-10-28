document.addEventListener("click", (event) => {
    const target = event.target;
    if (!(target instanceof HTMLElement)) {
        return;
    }

    const tab = target.closest(".questionnaire-tab");
    if (tab instanceof HTMLButtonElement) {
        activateQuestionnaireTab(tab);
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
        const heading = clone.querySelector("h3");
        if (heading) {
            heading.id = `section-heading-${sectionKey}-record-${recordNumber}`;
            heading.textContent = `${sectionKey}. ${section.dataset.sectionTitle} (additional record ${recordNumber})`;
            clone.setAttribute("aria-labelledby", heading.id);
        }

        clone.querySelectorAll("input, select, textarea").forEach((control) => {
            if (control instanceof HTMLInputElement ||
                control instanceof HTMLSelectElement ||
                control instanceof HTMLTextAreaElement) {
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

document.addEventListener("keydown", (event) => {
    const tab = event.target;
    if (!(tab instanceof HTMLButtonElement) || !tab.matches(".questionnaire-tab")) {
        return;
    }

    const tabs = Array.from(document.querySelectorAll(".questionnaire-tab"));
    const currentIndex = tabs.indexOf(tab);
    let nextIndex = currentIndex;
    if (event.key === "ArrowRight") {
        nextIndex = (currentIndex + 1) % tabs.length;
    } else if (event.key === "ArrowLeft") {
        nextIndex = (currentIndex - 1 + tabs.length) % tabs.length;
    } else if (event.key === "Home") {
        nextIndex = 0;
    } else if (event.key === "End") {
        nextIndex = tabs.length - 1;
    } else {
        return;
    }

    event.preventDefault();
    activateQuestionnaireTab(tabs[nextIndex]);
    tabs[nextIndex].focus();
});

function activateQuestionnaireTab(activeTab) {
    const activeSection = activeTab.dataset.section;
    document.querySelectorAll(".questionnaire-tab").forEach((tab) => {
        const isActive = tab === activeTab;
        tab.classList.toggle("is-active", isActive);
        tab.setAttribute("aria-selected", String(isActive));
        tab.tabIndex = isActive ? 0 : -1;
    });
    document.querySelectorAll("[data-section-panel]").forEach((panel) => {
        panel.hidden = panel.dataset.sectionPanel !== activeSection;
    });

    const activeSectionInput = document.getElementById("active-section");
    if (activeSectionInput instanceof HTMLInputElement) {
        activeSectionInput.value = activeSection;
    }

    const saveButton = document.querySelector(".questionnaire-actions button[value='continue']");
    const submitButton = document.querySelector(".questionnaire-actions button[value='submit']");
    const isLastSection = Number(activeSection) === document.querySelectorAll(".questionnaire-tab").length;
    if (saveButton instanceof HTMLButtonElement) {
        saveButton.hidden = isLastSection;
    }
    if (submitButton instanceof HTMLButtonElement) {
        submitButton.hidden = !isLastSection;
    }
}
