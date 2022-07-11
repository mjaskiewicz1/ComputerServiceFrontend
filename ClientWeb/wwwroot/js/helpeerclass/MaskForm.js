class MaskForm {
    static NipMask(idNip) {
        const selector = document.querySelector(idNip);
        if (selector == null) {
            return;
        }
        const im = new Inputmask("999-999-99-99");

        im.mask(selector);
    }

    static PostCodeMask(idPostCode) {

        const selector = document.querySelector(idPostCode);
        if (selector == null) {
            return;
        }

        const im = new Inputmask("99-999");
        im.mask(selector);
    }
}