class FormHelper {
    static blurValidate() {
        $("input[data-val=true]").blur(function() {
            $(this).valid();

        });
    }

    static requestFormChageViewRadioButton() {

        $("#Details").characterCounter();
        $("#FailureDescription").characterCounter();
        $(".clientTypeRadioButton").change(function() {
            if (this.value == "Firma") {
                FormHelper.clear("contentInvoiceCompany");
                $("#contentInvoiceCompany").show();
                $("#contentInvoiceIndividualClient").hide();

            } else {
                FormHelper.clear("contentInvoiceIndividualClient");
                $("#contentInvoiceIndividualClient").show();
                $("#contentInvoiceCompany").hide();

            }

        });
        $(".ShipmentTypeRadioButton").change(function() {
            if (this.value == "Wysyłka") {
                $("#ShipmentDetail").show();
                FormHelper.clear("ShipmentDetail");
                $("#ClearShipment").hide();
            } else {
                $("#ShipmentDetail").hide();

                $("#ClearShipment").show();
            }

        });
    }


    static clear(fieldid) {
        const container = document.getElementById(fieldid);
        const inputs = container.getElementsByTagName("input");
        const spans = container.querySelectorAll(".field-validation-error");

        for (var index = 0; index < inputs.length; ++index) {
            inputs[index].value = "";
        }
        for (var index = 0; index < spans.length; ++index) {
            spans[index].removeChild(spans[index].children[0]);
        }
    }

}