var KTAppOptions = {
    "colors": {
        "state": {
            "brand": "#2c77f4",
            "light": "#ffffff",
            "dark": "#282a3c",
            "primary": "#5867dd",
            "success": "#34bfa3",
            "info": "#36a3f7",
            "warning": "#ffb822",
            "danger": "#dc3545"
        },
        "base": {
            "label": ["#c5cbe3", "#a1a8c3", "#3d4465", "#3e4466"],
            "shape": ["#f0f3ff", "#d9dffa", "#afb4d4", "#646c9a"]
        }
    }
};

//if ($.fn.dataTable)
//    $.fn.dataTable.ext.errMode = 'none';

//Inputmask.extendDefaults({ clearIncomplete: true, showMaskOnHover: false });
$.fn.select2.defaults.set("language", "pt-BR");
$.fn.select2.defaults.set("placeholder", "SELECIONE...");
$.fn.select2.defaults.set("allowClear", true);
$.fn.select2.defaults.set("width", "100%");
$.fn.select2.defaults.set("theme", "bootstrap-5")
$.blockUI.defaults.message = `<h3><img src='${content}img/preloader.GIF' /></h3>`;
$.blockUI.defaults.baseZ = "2000";
$.blockUI.defaults.css.cursor = "default";
$.blockUI.defaults.css.border = "0px solid";
$.blockUI.defaults.css.backgroundColor = "transparent";
$.blockUI.defaults.overlayCSS.backgroundColor = "#E6E6E6";
$.blockUI.defaults.overlayCSS.cursor = "default";
aplicarCSSModalConfirmDialog();

$(document).ajaxStart(iniciarBlockUI).ajaxStop($.unblockUI);
$.ajaxSetup({ error: (xmlHttpRequest, textStatus, errorThrown) => alertaMensagens(xmlHttpRequest) });
$(document).submit(() => iniciarBlockUI());
/*$(window).bind("beforeunload", function () { setTimeout(() => iniciarBlockUI(), 100); });*/
$(document).on("reset", "form", function () {
    $("select:not(.input-sm.input-xsmall.input-inline, .custom-select)").val(null).trigger("change");
});
$(document).on("preInit.dt", () => iniciarBlockUI());
$(document).on("draw.dt", () => $.unblockUI());
$(document).on("error.dt", ".dataTables_wrapper", function (e, settings, techNote, message) {
    if (settings.jqXHR && settings.jqXHR.status !== 200) {
        alertaMensagens(settings.jqXHR);
        return;
    }

    mensagemPorTipo(message, "error");
});

function iniciarBlockUI() {
    if ($(".blockUI").length > 0)
        return;

    $.blockUI();
}
function obterMensagens(data) {
    try {
        return data.responseJSON.constructor === Array ? data.responseJSON : JSON.parse(data.responseJSON);
    }
    catch {
        try {
            return data.constructor === Array ? data : JSON.parse(data);
        }
        catch {
            return mensagemPorTipo(data.responseText, "error");
        }
    }
}
function alertaMensagens(data) {
    var mensagens = obterMensagens(data);
    if (mensagens != undefined)
        mensagens.forEach(m => mensagensPorTipo(m.Value, m.Key === 3 ? "error" : m.Key === 1 ? "success" : m.Key === 0 ? "info" : "alert"));
}
function Mensagem(options) {
    // Layout a ser aprensentado na tela
    var mLayout = 'topRight';
    if (options.layout)
        mLayout = options.layout;
    var mModal = false;
    if (options.dialog || options.modal) {
        mModal = true;
    }

    // TimeOut da mensagem
    var mTimeOut = 3000;
    if (options.timeout)
        mTimeOut = options.timeout;

    // Instância o tipo de mensagem a ser mostrada na tela.
    mType = 'alert';
    if (options.tipo || options.type) {
        var type = options.tipo || options.type;
        var intRegex = /^\d+$/;
        if (!intRegex.test(type)) type = type.toLowerCase();
        switch (type) {
            case 'alerta': case 'alert': case 'warning': case 1:
                mType = 'warning';
                break;
            case 'aviso': case 'info': case 2:
                mType = 'information';
                break;
            case 'erro': case 'error': case 'danger': case 3:
                mType = 'error';
                break;
            case 'sucesso': case 'success': case 4:
                mType = 'success';
                break;
            default:
                mType = 'alert';
                break;
        }
    }

    //
    // Id da mensagem
    mId = "noty_" + mType;
    if (options.id)
        mId = "noty_" + options.id;
    else
        mId = "noty_" + (new Date).getTime() * Math.floor(1e6 * Math.random()); // Montagem original do id da mensagem

    // Montar o array dos botões atribuidos em 'OPTIONS'
    var mButtons = false;

    if (options.OkEvent || options.okbutton || options.confirm) {
        if (mButtons == false)
            mButtons = Array();
        mButtons.push(Noty.button('OK',
            'btn btn-info',
            function () {
                n.close();
                if (options.OkEvent) eval(options.OkEvent);
            }));
    }

    if (options.CancelEvent || options.cancelbutton || options.confirm) {
        if (mButtons == false)
            mButtons = Array();
        mButtons.push(Noty.button('Cancelar',
            'btn btn-danger',
            function () {
                n.close();
                if (options.CancelEvent) options.CancelEvent();
            }));
    }

    if (options.dialog && !options.confirm && (options.CloseEvent || options.closebutton)) {
        if (mButtons == false)
            mButtons = Array();
        mButtons.push(Noty.button('Fechar',
            'btn btn-danger',
            function () {
                n.close();
                if (options.CloseEvent) options.CloseEvent();
            }));
    }
    // Fim do array

    // verifica se existe botões extras           
    if (typeof options.buttons === 'object' && options.buttons !== null) {
        var hasButtons = false;
        jQuery.each(options.buttons, function () {
            return !(hasButtons = true);
        });
        // se existir botões extras
        if (hasButtons) {
            jQuery.each(options.buttons, function (name, props) {
                if (mButtons == false)
                    mButtons = Array();
                mButtons.push(Noty.button(name,
                    'btn btn-info',
                    function () {
                        n.close();
                        if (props) eval(props);
                    }));
            });
        }
    }

    var n = new Noty({
        id: mId,
        layout: mLayout,
        theme: 'relax',
        type: mType,
        text: options.texto,
        timeout: mTimeOut,
        modal: mModal,
        buttons: mButtons
    });
    n.show();
}
function mensagemPorTipo(texto, tipo) {
    Mensagem({ texto: texto, tipo: tipo });
}
function mensagensPorTipo(arrMsg, tipo) {
    var msg = "<ul>";

    for (var i = 0; i < arrMsg.length; i++) {
        msg += "<li>" + arrMsg[i] + "</li>";
    }

    msg += "</ul>";

    var n = new Noty({
        theme: 'relax',
        type: tipo,
        text: msg,
        modal: true
    });
    n.show();
}
function dialogConfirmar(object) {
    var n = new Noty({
        theme: 'relax',
        type: verificaSeEstaPreenchido(object.tipo) ? object.tipo : 'warning',
        layout: 'bottom',
        text: object.texto,
        modal: true,
        callbacks: {
            onClick: function () {
                if (verificaSeEstaPreenchido(object.funcaoCloseClick))
                    eval(object.funcaoCloseClick);
            }
        },
        buttons: [
            Noty.button('SIM', 'btn btn-success', function () {
                if (verificaSeEstaPreenchido(object.funcaoOk))
                    eval(object.funcaoOk);

                n.close();
            }),
            Noty.button('NÃO', 'btn btn-danger', () => {
                if (verificaSeEstaPreenchido(object.funcaoCloseClick))
                    eval(object.funcaoCloseClick);

                n.close()
            })
        ]
    });
    n.show();
}
function mensagemConfirmar(object) {
    var n = new Noty({
        theme: 'relax',
        type: 'warning',
        layout: 'bottom',
        modal: true,
        text: object.texto,
        buttons: [
            Noty.button('SIM', 'btn btn-success', function () {
                if (verificaSeEstaPreenchido(object.funcaoOk))
                    eval(object.funcaoOk);
                n.close();
            }),

            Noty.button('NÃO', 'btn btn-danger', function () {
                if (verificaSeEstaPreenchido(object.funcaoCancelar))
                    eval(object.funcaoCancelar);
                n.close();
            })
        ]
    });
    n.show();
}
function dialogInfo(object) {
    var n = new Noty({
        theme: 'relax',
        type: verificaSeEstaPreenchido(object.tipo) ? object.tipo : 'warning',
        layout: 'bottom',
        text: object.texto,
        modal: true,
        callbacks: {
            onClick: function () {
                if (verificaSeEstaPreenchido(object.funcaoCloseClick))
                    eval(object.funcaoCloseClick);
            }
        },
        buttons: [
            Noty.button('OK', 'btn btn-success', function () {
                if (verificaSeEstaPreenchido(object.funcaoOk))
                    eval(object.funcaoOk);
                n.close();
            })
        ]
    });
    n.show();
}
function somenteNumeros(e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }
}
function validarCPF(cpf) {
    cpf = cpf.replace(/[^\d]+/g, "");

    if (!verificaSeEstaPreenchido(cpf)) return true;

    // Elimina CPFs inválidos conhecidos
    if (cpf.length != 11 ||
        cpf == "00000000000" ||
        cpf == "11111111111" ||
        cpf == "22222222222" ||
        cpf == "33333333333" ||
        cpf == "44444444444" ||
        cpf == "55555555555" ||
        cpf == "66666666666" ||
        cpf == "77777777777" ||
        cpf == "88888888888" ||
        cpf == "99999999999")
        return false;

    // Valida 1o digito	
    add = 0;

    for (i = 0; i < 9; i++)
        add += parseInt(cpf.charAt(i)) * (10 - i);

    rev = 11 - (add % 11);

    if (rev == 10 || rev == 11)
        rev = 0;

    if (rev != parseInt(cpf.charAt(9)))
        return false;

    // Valida 2o digito
    add = 0;

    for (i = 0; i < 10; i++)
        add += parseInt(cpf.charAt(i)) * (11 - i);

    rev = 11 - (add % 11);

    if (rev == 10 || rev == 11)
        rev = 0;

    if (rev != parseInt(cpf.charAt(10)))
        return false;

    return true;
}
function validarCNPJ(cnpj) {
    cnpj = cnpj.replace(/[^\d]+/g, "");

    if (!verificaSeEstaPreenchido(cnpj)) return true;

    if (cnpj.length != 14)
        return false;

    // Elimina CNPJs inválidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;

    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }

    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;

    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }

    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

    if (resultado != digitos.charAt(1))
        return false;

    return true;
}
function verificaSeEstaPreenchido(valor) {
    return valor != undefined && valor != null && valor != "";
}
function rebindForm(form) {
    try {
        form.unbind();
        form.data("validator", null);
        $(".field-validation-error").html("");

        if ($.validator === undefined || $.validator.unobtrusive === undefined) return;

        $.validator.unobtrusive.parse(document);
        form.validate(form.data("unobtrusiveValidation").options);
    } catch (e) { }
}
function ArquivoChange(campo) {
    var input = $(campo);

    if (input.get(0).files.length > 0) {
        var nomeArquivo = input.get(0).files[0].name.toLowerCase();

        if (!nomeArquivo.includes(".pdf")) {
            mensagemPorTipo("São aceitos somente arquivos no formato PDF", "alert");

            label = "";
            input.val(null);
            $(campo).parent().find(".custom-file-label").removeClass("selected").html("Escolha um arquivo...");
        }

        var maxSize = input.data('max-size');
        var fileSize = input.get(0).files[0].size;

        if (fileSize > maxSize) {
            mensagemPorTipo("O tamanho máximo do arquivo é 15 MB", "alert");

            label = "";
            input.val(null);
            $(campo).parent().find(".custom-file-label").removeClass("selected").html("Escolha um arquivo...");
        }

        $(campo).parent().find(".custom-file-label").addClass("selected").html($(campo).val());
    }
}
function aplicarCSSModalConfirmDialog() {
    $("a[data-ajax-confirm]").click(function (ev) {
        if ($(this).prop("disabled") == true)
            return false;

        var href = $(this).attr("data-ajax-url");

        if (!verificaSeEstaPreenchido(href))
            href = $(this).attr("href");;

        var dataAjax = $(this).attr("data-ajax");
        var dataAjaxBegin = $(this).attr("data-ajax-begin");
        var dataAjaxComplete = $(this).attr("data-ajax-complete");
        var dataAjaxMethod = $(this).attr("data-ajax-method");
        var dataAjaxLoading = $(this).attr("data-ajax-loading");
        var dataAjaxMode = $(this).attr("data-ajax-mode");
        var dataAjaxUpdate = $(this).attr("data-ajax-update");
        var dataAjaxSucccess = $(this).attr("data-ajax-success");
        var dataAjaxFailure = $(this).attr("data-ajax-failure");
        var n = new Noty({
            theme: "relax",
            type: "warning",
            layout: "bottom",
            modal: true,
            text: $(this).data("ajax-confirm"),
            buttons: [
                Noty.button('SIM', 'btn btn-success', function () {
                    n.close();

                    if (!$("#dataConfirmOK").length)
                        $("body").append('<a class="d-none" id="dataConfirmOK"></a>');

                    $("#dataConfirmOK").attr("href", href);
                    $("#dataConfirmOK").attr("data-ajax-url", href);
                    $("#dataConfirmOK").attr("data-ajax", dataAjax);
                    $("#dataConfirmOK").attr("data-ajax-begin", dataAjaxBegin);
                    $("#dataConfirmOK").attr("data-ajax-complete", dataAjaxComplete);
                    $("#dataConfirmOK").attr("data-ajax-method", dataAjaxMethod);
                    $("#dataConfirmOK").attr("data-ajax-loading", dataAjaxLoading);
                    $("#dataConfirmOK").attr("data-ajax-mode", dataAjaxMode);
                    $("#dataConfirmOK").attr("data-ajax-update", dataAjaxUpdate);
                    $("#dataConfirmOK").attr("data-ajax-success", dataAjaxSucccess);
                    $("#dataConfirmOK").attr("data-ajax-failure", dataAjaxFailure);
                    $("#dataConfirmOK").click();
                }),
                Noty.button('NÃO', 'btn btn-danger', () => n.close())
            ]
        }).show();

        return false;
    });
}
function aplicarCSSModalConfirmDialogPeloNomeDoElemento(nomeDoElemento) {
    $("a[name='" + nomeDoElemento + "']").click(function (ev) {
        if ($(this).prop("disabled") == true)
            return false;

        var href = $(this).attr("data-ajax-url");

        if (!verificaSeEstaPreenchido(href))
            href = $(this).attr("href");;

        var dataAjax = $(this).attr("data-ajax");
        var dataAjaxBegin = $(this).attr("data-ajax-begin");
        var dataAjaxComplete = $(this).attr("data-ajax-complete");
        var dataAjaxMethod = $(this).attr("data-ajax-method");
        var dataAjaxLoading = $(this).attr("data-ajax-loading");
        var dataAjaxMode = $(this).attr("data-ajax-mode");
        var dataAjaxUpdate = $(this).attr("data-ajax-update");
        var dataAjaxSucccess = $(this).attr("data-ajax-success");
        var dataAjaxFailure = $(this).attr("data-ajax-failure");
        var n = new Noty({
            theme: "relax",
            type: "warning",
            layout: "bottom",
            modal: true,
            text: $(this).data("ajax-confirm"),
            buttons: [
                Noty.button('SIM', 'btn btn-success', function () {
                    n.close();

                    if (!$("#dataConfirmOK").length)
                        $("body").append('<a class="d-none" id="dataConfirmOK"></a>');

                    $("#dataConfirmOK").attr("href", href);
                    $("#dataConfirmOK").attr("data-ajax-url", href);
                    $("#dataConfirmOK").attr("data-ajax", dataAjax);
                    $("#dataConfirmOK").attr("data-ajax-begin", dataAjaxBegin);
                    $("#dataConfirmOK").attr("data-ajax-complete", dataAjaxComplete);
                    $("#dataConfirmOK").attr("data-ajax-method", dataAjaxMethod);
                    $("#dataConfirmOK").attr("data-ajax-loading", dataAjaxLoading);
                    $("#dataConfirmOK").attr("data-ajax-mode", dataAjaxMode);
                    $("#dataConfirmOK").attr("data-ajax-update", dataAjaxUpdate);
                    $("#dataConfirmOK").attr("data-ajax-success", dataAjaxSucccess);
                    $("#dataConfirmOK").attr("data-ajax-failure", dataAjaxFailure);
                    $("#dataConfirmOK").click();
                }),
                Noty.button('NÃO', 'btn btn-danger', () => n.close())
            ]
        }).show();

        return false;
    });
}
function anoEhFuturo(ano) {
    if (!verificaSeEstaPreenchido(ano))
        return false;

    return Number(ano) > new Date().getFullYear();
}
function recarregarPagina() {
    iniciarBlockUI();
    window.location.reload();
}
function exibirMensagemSucessoRecarregandoPaginaAposClicarEmOk(data) {
    dialogInfo({ tipo: "success", texto: data, funcaoOk: "recarregarPagina()" });
}
