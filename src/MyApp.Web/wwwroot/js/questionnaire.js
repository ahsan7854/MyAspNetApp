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

    const addTravelRowButton = target.closest(".add-travel-row");
    if (addTravelRowButton instanceof HTMLButtonElement) {
        const table = addTravelRowButton.closest("[data-travel-list]")?.querySelector(".travel-history-table");
        const rows = table?.querySelectorAll("[data-travel-row]");
        const lastRow = rows?.[rows.length - 1];
        const currentNumber = Number(lastRow?.dataset.recordNumber) || 1;
        const nextNumber = currentNumber + 1;
        if (!(table instanceof HTMLTableElement) ||
            !(lastRow instanceof HTMLTableRowElement) ||
            nextNumber > 20) {
            return;
        }

        const newRow = lastRow.cloneNode(true);
        if (!(newRow instanceof HTMLTableRowElement)) {
            return;
        }
        newRow.dataset.recordNumber = String(nextNumber);
        const serialNumber = newRow.querySelector("th[scope='row']");
        if (serialNumber) {
            serialNumber.textContent = String(nextNumber);
        }
        newRow.querySelectorAll("input, select, textarea").forEach((control) => {
            if (control instanceof HTMLInputElement ||
                control instanceof HTMLSelectElement ||
                control instanceof HTMLTextAreaElement) {
                const match = control.name.match(/^Answers\[(travelHistory[A-Za-z0-9]*)(?:__\d+)?\]$/);
                if (match) {
                    control.name = `Answers[${match[1]}__${nextNumber}]`;
                }
                if (control.id) {
                    control.id = control.id.replace(/__\d+$/, "") + `__${nextNumber}`;
                }
                control.value = "";
                control.disabled = false;
            }
        });
        lastRow.insertAdjacentElement("afterend", newRow);
        if (nextNumber === 20) {
            addTravelRowButton.disabled = true;
        }
        return;
    }

    const addChildButton = target.closest(".add-child");
    if (addChildButton instanceof HTMLButtonElement) {
        const childList = addChildButton.closest("[data-child-list]");
        const existingChildren = childList?.querySelectorAll("[data-child-record]");
        if (!(childList instanceof HTMLElement) || !existingChildren?.length) {
            return;
        }

        const lastChild = existingChildren[existingChildren.length - 1];
        const recordNumbers = Array.from(existingChildren).map((child) =>
            Number(child.dataset.recordNumber) || 1);
        const recordNumber = Math.max(...recordNumbers) + 1;
        if (recordNumber > 20 || !(lastChild instanceof HTMLElement)) {
            return;
        }

        const clone = lastChild.cloneNode(true);
        if (!(clone instanceof HTMLElement)) {
            return;
        }

        clone.dataset.recordNumber = String(recordNumber);
        const heading = clone.querySelector("h4");
        if (heading) {
            heading.id = `child-heading-${recordNumber}`;
            heading.textContent = `Child ${recordNumber}`;
            clone.setAttribute("aria-labelledby", heading.id);
        }
        clone.querySelectorAll("input, select, textarea").forEach((control) => {
            if (control instanceof HTMLInputElement ||
                control instanceof HTMLSelectElement ||
                control instanceof HTMLTextAreaElement) {
                const match = control.name.match(/^Answers\[([A-Za-z][A-Za-z0-9]*)(?:__\d+)?\]$/);
                if (match) {
                    control.name = `Answers[${match[1]}__${recordNumber}]`;
                }
                if (control.id) {
                    control.id = control.id.replace(/__\d+$/, "") + `__${recordNumber}`;
                }
                control.value = "";
                control.disabled = false;
            }
        });
        clone.querySelectorAll("label[for]").forEach((label) => {
            const currentFor = label.getAttribute("for");
            if (currentFor) {
                label.setAttribute("for", currentFor.replace(/__\d+$/, "") + `__${recordNumber}`);
            }
        });
        clone.querySelectorAll("[data-conditional-question]").forEach((row) => {
            if (row instanceof HTMLTableRowElement && row.dataset.dependsOn) {
                row.dataset.dependsOn = row.dataset.dependsOn.replace(/__\d+$/, "") + `__${recordNumber}`;
                row.hidden = true;
                row.querySelectorAll("input, select, textarea").forEach((control) => {
                    if (control instanceof HTMLInputElement ||
                        control instanceof HTMLSelectElement ||
                        control instanceof HTMLTextAreaElement) {
                        control.disabled = true;
                    }
                });
            }
        });
        let removeButton = clone.querySelector(".remove-child");
        if (!(removeButton instanceof HTMLButtonElement)) {
            removeButton = document.createElement("button");
            removeButton.type = "button";
            removeButton.className = "button button-secondary remove-child";
            removeButton.textContent = "Remove this child";
            clone.querySelector(".questionnaire-section-heading")?.append(removeButton);
        }

        lastChild.insertAdjacentElement("afterend", clone);
        clone.querySelectorAll("[data-conditional-question]").forEach((row) => {
            if (row instanceof HTMLTableRowElement && row.dataset.dependsOn) {
                updateConditionalQuestions(row.dataset.dependsOn);
            }
        });
        if (recordNumber === 20) {
            addChildButton.disabled = true;
        }
        return;
    }

    const removeChildButton = target.closest(".remove-child");
    if (removeChildButton instanceof HTMLButtonElement) {
        const childRecord = removeChildButton.closest("[data-child-record]");
        const childList = removeChildButton.closest("[data-child-list]");
        const childRecords = childList?.querySelectorAll("[data-child-record]");
        if (childRecord instanceof HTMLElement && childRecords?.length === 1) {
            childRecord.querySelectorAll("input, select, textarea").forEach((control) => {
                if (control instanceof HTMLInputElement ||
                    control instanceof HTMLSelectElement ||
                    control instanceof HTMLTextAreaElement) {
                    control.value = "";
                }
            });
        } else {
            childRecord?.remove();
        }
        if (childList instanceof HTMLElement) {
            childList.querySelector(".add-child")?.removeAttribute("disabled");
        }
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

document.addEventListener("change", (event) => {
    const target = event.target;
    if (target instanceof HTMLSelectElement && target.id) {
        updateConditionalQuestions(target.id);
        if (target.id === "answer-hasChildren") {
            updateChildListVisibility(target);
        }
        if (target.id === "answer-visitedCountryLast10Years") {
            updateTravelListVisibility(target);
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

document.querySelectorAll("[data-conditional-question]").forEach((row) => {
    if (row instanceof HTMLTableRowElement && row.dataset.dependsOn) {
        updateConditionalQuestions(row.dataset.dependsOn);
    }
});

const hasChildrenQuestion = document.getElementById("answer-hasChildren");
if (hasChildrenQuestion instanceof HTMLSelectElement) {
    updateChildListVisibility(hasChildrenQuestion);
}

const visitedCountryQuestion = document.getElementById("answer-visitedCountryLast10Years");
if (visitedCountryQuestion instanceof HTMLSelectElement) {
    updateTravelListVisibility(visitedCountryQuestion);
}

function updateTravelListVisibility(question) {
    const isVisible = question.value === "Yes";
    document.querySelectorAll("[data-travel-list]").forEach((travelList) => {
        if (!(travelList instanceof HTMLElement)) {
            return;
        }

        travelList.hidden = !isVisible;
        travelList.querySelectorAll("input, select, textarea").forEach((control) => {
            if (control instanceof HTMLInputElement ||
                control instanceof HTMLSelectElement ||
                control instanceof HTMLTextAreaElement) {
                control.disabled = !isVisible;
                if (!isVisible) {
                    control.value = "";
                }
            }
        });
        const addButton = travelList.querySelector(".add-travel-row");
        if (addButton instanceof HTMLButtonElement) {
            addButton.disabled = !isVisible ||
                travelList.querySelectorAll("[data-travel-row]").length >= 20;
        }
    });
}

function updateChildListVisibility(question) {
    const isVisible = question.value === "Yes";
    document.querySelectorAll("[data-child-list]").forEach((childList) => {
        if (!(childList instanceof HTMLElement)) {
            return;
        }

        childList.hidden = !isVisible;
        childList.querySelectorAll("input, select, textarea").forEach((control) => {
            if (control instanceof HTMLInputElement ||
                control instanceof HTMLSelectElement ||
                control instanceof HTMLTextAreaElement) {
                control.disabled = !isVisible;
                if (!isVisible) {
                    control.value = "";
                }
            }
        });
        childList.querySelectorAll("[data-conditional-question]").forEach((row) => {
            if (row instanceof HTMLTableRowElement && row.dataset.dependsOn) {
                updateConditionalQuestions(row.dataset.dependsOn);
            }
        });
        const addButton = childList.querySelector(".add-child");
        if (addButton instanceof HTMLButtonElement) {
            addButton.disabled = !isVisible ||
                childList.querySelectorAll("[data-child-record]").length >= 20;
        }
    });
}

function updateConditionalQuestions(dependencyId) {
    document.querySelectorAll("[data-conditional-question]").forEach((row) => {
        if (!(row instanceof HTMLTableRowElement) || row.dataset.dependsOn !== dependencyId) {
            return;
        }

        const dependency = document.getElementById(dependencyId);
        const childList = row.closest("[data-child-list]");
        const isVisible = dependency instanceof HTMLSelectElement &&
            dependency.value === row.dataset.dependsValue &&
            !(childList instanceof HTMLElement && childList.hidden);
        row.hidden = !isVisible;
        row.querySelectorAll("input, select, textarea").forEach((control) => {
            if (control instanceof HTMLInputElement ||
                control instanceof HTMLSelectElement ||
                control instanceof HTMLTextAreaElement) {
                control.disabled = !isVisible;
                if (!isVisible) {
                    control.value = "";
                }
            }
        });
    });
}

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
    const isLastSection = Number(activeSection) === document.querySelectorAll(".questionnaire-tab").length;
    if (saveButton instanceof HTMLButtonElement) {
        saveButton.hidden = isLastSection;
    }
}
