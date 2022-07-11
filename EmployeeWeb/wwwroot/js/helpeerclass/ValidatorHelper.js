class ValidatorHelper {
    constructor() {
    }

    static ShowErrors(formJQuery, errors) {
        const validator = formJQuery.validate();
        validator.showErrors(errors);
    }

    static ShowErrorsWithSummary(formJQuery, errors) {
        const validator = formJQuery.validate();
        validator.showErrors(errors);

        const summary = formJQuery[0].querySelector("[data-valmsg-summary=true]");
        summary.classList.remove("validation-summary-valid");
        summary.classList.add("validation-summary-errors");
        const ul = summary.querySelector(".validation-summary-errors>ul");

        for (let error in errors) {
            const size = errors[error].length;
            for (let i = 0; i < size; i++) {
                const li = document.createElement("li");
                ul.appendChild(li);
                li.innerHTML = errors[error][i];

            }


        }
    }

    static ResetValidationSummary(errorContainerList) {
        const div = document.querySelector(errorContainerList);
        console.log(div);
        div.style.display = "none";
        const ul = div.querySelector("ul");
        while (ul.firstChild) {
            ul.firstChild.remove();
        }
    }

    static ShowErrorsSummary(form, errors) {
        const summary = form.querySelector("[data-valmsg-summary=true]");
        summary.classList.remove("validation-summary-valid");
        summary.classList.add("validation-summary-errors");
        const ul = summary.querySelector(".validation-summary-errors>ul");
        while (ul.firstChild) {
            ul.firstChild.remove();
        }
        for (let i = 0; i < errors.length; i++) {

            const size = errors[i].value.length;

            for (let j = 0; j < size; j++) {
                const li = document.createElement("li");
                ul.appendChild(li);
                li.innerHTML = errors[i].value[j];

            }
        }
    }

    static showErrorFormInput(errors) {


        const errorsValue = [];
        console.log(errors);
        for (let error in errors) {


            const key = errors[error].key;


            const valueForInput = errors[error].value[0];


            const input = document.getElementById(key);
            console.log(key);


            if (input != null) {
                this.ChangeInputStatusInvalid(input, key, valueForInput);
            } else {
                const size = errors[error].value.length;
                for (let j = 0; j < size; j++) {
                    errorsValue.push(errors[error].value[j]);
                }
            }


        }
        console.table(errorsValue);
        return errorsValue;


    }

    static showErrorsModelAll(errors, errorContainerList) {

        const valueError = this.showErrorFormInputAll(errors);

        this.ShowErrorsSummary(valueError, errorContainerList);
    }

    static showErrorsModelOnly(errors, errorContainerList) {

        const valueError = this.showErrorFormInput(errors);

        if (valueError.length !== 0) {

            this.ShowErrorsSummary(valueError, errorContainerList);
        }

    }

    static showErrorFormInputAll(errors) {

        const errorsValue = [];
        console.table(errors);
        for (let error in errors) {


            const key = errors[error].key;

            const size = errors[error].value.length;
            const valueForInput = errors[error].value[0];


            const input = document.getElementById(key);
            console.log(key);
            for (let j = 0; j < size; j++) {
                errorsValue.push(errors[error].value[j]);
            }

            if (typeof (input) == "undefined" || input == null) {

                continue;


            }


            this.ChangeInputStatusInvalid(input, key, valueForInput);

        }

        return errorsValue;


    }

    static ShowErrorsSummary(errors, errorContainerList) {
        const div = document.querySelector(errorContainerList);
        const ul = div.querySelector("ul");
        while (ul.firstChild) {
            ul.firstChild.remove();
        }
        for (let i = 0; i < errors.length; i++) {
            const li = document.createElement("li");
            ul.appendChild(li);
            console.log(errors[i]);
            li.innerHTML = errors[i];
        }
        if (ul.firstChild) {
            div.style = "block";
        }
    }

    static ChangeInputStatusInvalid(input, key, valueForInput) {
        const querySelectorSpan = `[data-valmsg-for=${key}]`;
        const span = document.querySelector(querySelectorSpan);
        if (!input.classList.contains("invalid")) {
            span.textContent = valueForInput;
            input.classList.remove("valid");
            input.classList.add("input-validation-error");
        }


    }

    static ChangeInputStatusValid(input) {

        const querySelectorSpan = `[data-valmsg-for=${input.id}]`;
        input.classList.remove("input-validation-error");
        input.classList.add("valid");
        const span = document.querySelector(querySelectorSpan);
        span.textContent = "";

    }


    static BlurValidNip(input, selectId = "#ClientTypeViewModel") {
        const selectValue = document.querySelector(selectId).value;
        const inputId = input.id;
        if (selectValue === "1") {
            const nip = input.value;
            if (nip.length === 0) {

                this.ChangeInputStatusInvalid(input, "Nip", "Nip jest wymagany");
            } else {
                const validation = this.ValidationNip(input.value);
                console.log(validation);

                if (!validation) {
                    this.ChangeInputStatusInvalid(input, "Nip", "Nip ma nieprawidłowy format");
                } else {
                    this.ChangeInputStatusValid(input);
                }
            }
        }

    }

    static ValidationNip(nipValue) {
        if (typeof nipValue !== "string")
            return false;

        const nip = nipValue.replace(/[\ \-]/gi, "");

        const weight = [6, 5, 7, 2, 3, 4, 5, 6, 7];
        let sum = 0;
        const controlNumber = parseInt(nip.substring(9, 10));
        const weightCount = weight.length;
        for (let i = 0; i < weightCount; i++) {
            sum += (parseInt(nip.substr(i, 1)) * weight[i]);
        }

        return sum % 11 === controlNumber;
    }

    static BlurValidNameCompany(input, selectId = "#ClientTypeViewModel") {

        const selectValue = document.querySelector(selectId).value;
        const inputId = input.id;
        if (selectValue === "1") {
            const nip = input.value;
            if (nip.length === 0) {

                this.ChangeInputStatusInvalid(input, inputId, "Nazwa firmy jest wymgana");
            } else {
                this.ChangeInputStatusValid(input);

            }
        }
    }

    static ChangeHtmlContentDependOnSelect(selectId = "#ClientTypeViewModel", nipContainerId =
    "#NipContainer", nameCompanyId = "#NameCompany", clientTypeContainerId ="#ClientTypeContainer") {
        const select = document.querySelector(selectId);
        const nipContainer = document.querySelector(nipContainerId);
        const nameCompanyContainer = document.querySelector(nameCompanyId);
        const clientTypeContainer = document.querySelector(clientTypeContainerId);
        select.addEventListener("change",
            function() {
                const valueSelect = select.value;
                if (valueSelect === "1") {
                    clientTypeContainer.classList.add("m3", "xl4");
                    console.log(select);
                    nipContainer.style.display = "block";
                    nameCompanyContainer.style.display = "block";

                } else {
                    clientTypeContainer.classList.remove("m3", "xl4");
                    console.log(select);
                    const inputNip = nipContainer.querySelector("input");
                    inputNip.value = "";
                    const inputNameCompany = nameCompanyContainer.querySelector("input");
                    inputNameCompany.value = "";
                    ValidatorHelper.ChangeInputStatusValid(inputNameCompany);
                    ValidatorHelper.ChangeInputStatusValid(inputNip);
                    nipContainer.style.display = "none";
                    nameCompanyContainer.style.display = "none";


                }
            });
    }

}