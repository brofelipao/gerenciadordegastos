(function ($) {
    $.validator.addMethod("data-val-validar-cpf", (value, element, params) => validarCPF(value), (params, element) => $(element).attr("data-val-validar-cpf"));
    $.validator.unobtrusive.adapters.addBool("data-val-validar-cpf");

    $.validator.addMethod("data-val-validar-cnpj", (value, element, params) => validarCNPJ(value), (params, element) => $(element).attr("data-val-validar-cnpj"));
    $.validator.unobtrusive.adapters.addBool("data-val-validar-cnpj");

    $.validator.addMethod("data-val-ano-nao-pode-ser-futuro", (value, element, params) => !anoEhFuturo(value), (params, element) => $(element).attr("data-val-ano-nao-pode-ser-futuro"));
    $.validator.unobtrusive.adapters.addBool("data-val-ano-nao-pode-ser-futuro");
})(jQuery);