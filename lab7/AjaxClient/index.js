const JSON_URL = "http://localhost:2119/MyRestService.svc/json";
const XML_URL = "http://localhost:2119/MyRestService.svc";

let currentFormat = "json";

function changeFormat() {
    currentFormat = currentFormat === "json" ? "xml" : "json";
    document.getElementById("operations-header").innerHTML = "Operations (current format: " + currentFormat + ")";
}

function toggleForm() {
    const formContainer = document.getElementById('form-container');
    formContainer.style.display = 'flex';
}

function hideForm() {
    const formContainer = document.getElementById('form-container');
    formContainer.style.display = 'none';
}

function clearForm() {
    const form = document.getElementById("person-form");
    const inputs = form.getElementsByTagName("input");

    for (let i = 0; i < inputs.length; i++) {
        inputs[i].value = "";
    }
}

function removeInputsFromForm() {
    const form = document.getElementById("inputs-container");
    while (form.firstChild) {
        form.firstChild.remove();
    }
}

function createInputLabel(labelText, inputId, inputType, isRequired, min, max) {
    const inputsContainer = document.getElementById("inputs-container");
    const label = document.createElement("label");
    label.setAttribute("for", inputId);
    label.textContent = labelText;
    inputsContainer.appendChild(label);

    const input = document.createElement("input");
    input.setAttribute("type", inputType);
    input.setAttribute("id", inputId);
    input.required = isRequired;
    if (min || min===0) {
        input.min = min;
    }
    if (max) {
        input.max = max;
    }
    inputsContainer.appendChild(input);

    inputsContainer.appendChild(document.createElement("br"));
}

document.getElementById("getAllBtn").addEventListener("click", function() {
    hideForm();

});

document.getElementById("getByIdBtn").addEventListener("click", function() {
    hideForm();
    removeInputsFromForm();
    document.getElementById("form-title").textContent = "Get Person by Id";
    createInputLabel( "Id", "id", "number", true);
    toggleForm();
});

document.getElementById("filterByNameBtn").addEventListener("click", function() {
    hideForm();
    removeInputsFromForm();
    document.getElementById("form-title").textContent = "Filter People by Name";
    createInputLabel("Name", "name", "text", true);
    toggleForm();
});

document.getElementById("countBtn").addEventListener("click", function() {
    hideForm();
});

document.getElementById("addBtn").addEventListener("click", function() {
    hideForm();
    removeInputsFromForm();
    document.getElementById("form-title").textContent = "Add Person";
    createInputLabel("Name", "name", "text", true);
    createInputLabel("Age", "age", "number", true, 0, 120);
    createInputLabel("Email", "email", "email", true);
    toggleForm();
});

document.getElementById("updateBtn").addEventListener("click", function() {
    hideForm();
    removeInputsFromForm()
    document.getElementById("form-title").textContent = "Update Person";
    createInputLabel( "Id", "id", "number", true);
    createInputLabel("Name", "name", "text", true);
    createInputLabel("Age", "age", "number", true, 0, 120);
    createInputLabel("Email", "email", "email", true);
    toggleForm();
});

document.getElementById("deleteBtn").addEventListener("click", function() {
    hideForm();
    removeInputsFromForm()
    document.getElementById("form-title").textContent = "Delete Person";
    createInputLabel( "Id", "id", "number", true);
    toggleForm();
});

document.getElementById("changeFormatBtn").addEventListener("click", function() {
    hideForm();
    changeFormat();
});


