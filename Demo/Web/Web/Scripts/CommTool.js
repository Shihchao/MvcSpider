$.extend({
    //简易Modal插件
    EasyModal: function (opt) {
        if (typeof (opt.Id) != 'undefined' && opt.Id != null) {
            var old = $(document.body).find('#' + opt.Id + "_Modal");
            if (old.length > 0) $(old).remove();
        }

        var modal = ModalTool.BuildModal(opt);
        $(document.body).append(modal);
        $(modal).modal('show');
    },

    CustomModal: function (opt) {
        if (typeof (opt.Id) != 'undefined' && opt.Id != null) {
            var old = $(document.body).find('#' + opt.Id + "_Modal");
            if (old.length > 0) $(old).remove();
        }

        var modal = ModalTool.BuildCustomModal(opt);
        $(document.body).append(modal);
        $(modal).modal('show');
    },

    BuildArray: function (str) {
        var dic = new Array();

        var kvps = str.split(',');
        $(kvps).each(function () {
            var kvp = this.split(':');
            if (kvp.length > 1) {
                dic[kvp[0]] = kvp[1];
            }
        });
        return dic;
    },

    //验证数据
    VerifyVal: function (e) {
        var form = $(e);
        var result = true;
        $(form).find('[Verify="True"]').each(function () {
            var v = $(this).val();
            if (typeof (v) == 'undefined' || v == null || v.length == 0) {
                $(this).addClass('hightLight');
                result = false;
            }
        });
        if (!result) {
            BTUTILS.ShowMessage({ title: "请输入数据！", type: "warning" });
        }
        return result;
    },

});

//Js数组插入扩展
Array.prototype.insert = function (index, item) {
    this.splice(index, 0, item);
};
//Js数组移除扩展
Array.prototype.remove = function (str) {
    var idx = this.indexOf(str);
    if (idx > -1) {
        this.splice(idx, 1);
    }
};


var ModalFactory = {
    GetModal: function (id, title, needLg) {
        var head = $('<div class="modal-header"></div>')
            .append('<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>')
            .append('<h4 class="modal-title">' + title + '</h4>');

        var body = $('<div class="modal-body"></div>');

        var foot = $('<div class="modal-footer"></div >')

        var content = $('<div class="modal-content"></div>').append(head).append(body).append(foot);
        var dialog = $('<div class="modal-dialog" role="document"></div>').append(content);
        if (needLg) {
            dialog.addClass('modal-lg');
        }
        var modal = $('<div id= "' + id + '_Modal" class="modal fade" tabindex= "-1" role= "dialog" >').append(dialog);

        return {
            Modal: modal,
            Body: body,
            Foot: foot
        };
    },
}

var SubmitTypeEnum = {
    Form: "form",
    Ajax: "ajax",
};

var ModalTool = {

    closable: false,

    //请求成功回调
    Callback_Success: null,

    default: {
        Id: "iModal",
        Url: "",
        Title: "操作",
        Data: null,
        NeedClose: false,
        SubmitType: SubmitTypeEnum.Form,
        Callback_Success: null,
        NeedLg: false,
        AppendObj: null,
    },

    customDefault: {
        Id: "customModal",
        Title: "自定义弹出框",
        BodyObj: null,
        FootObj: null,
        NeedLg: false,
    },

    //默认数据格式
    dataFormat: {
        text: "格式错误",
        name: "格式错误",
        type: "text",
        class: "",
        val: "",
        attrs: "",
        iFormat: "str",
        verify: "true",
    },

    BuildModal: function (opt) {
        var o = $.extend({}, this.default, opt);
        this.closable = o.NeedClose;
        this.Callback_Success = o.Callback_Success;

        //body内容生成
        var bodyContent;
        switch (o.SubmitType) {
            case SubmitTypeEnum.Form: bodyContent = $('<form id="' + o.Id + '_Form" method="post" action="' + o.Url
                + '" enctype="multipart/form-data" novalidate="novalidate" class="content-defmargin"></form>'); break;
            case SubmitTypeEnum.Ajax: bodyContent = $('<div id="' + o.Id + '_Form" action="' + o.Url + '" class="content-defmargin" ></div>'); break;
            default: bodyContent = $('<div class="content-defmargin"></div>'); break;
        }
        if (o.Data != null) {
            $(o.Data).each(function () {
                bodyContent.append(ModalTool.BuildContent(this));
            });
        }
        if (o.AppendObj != null) {
            bodyContent.append(o.AppendObj);
        }

        var modalObj = ModalFactory.GetModal(o.Id, o.Title, o.NeedLg);
        modalObj.Body.append(bodyContent);
        modalObj.Foot.append('<button type= "button" class="btn btn-primary" onclick= "modalSubmit(' + "'" + o.Id + "','" + o.SubmitType + "'" + ')" >保存</button >');

        return modalObj.Modal;
    },

    BuildCustomModal: function (opt) {
        opt = $.extend({}, this.customDefault, opt);
        var modalObj = ModalFactory.GetModal(opt.Id, opt.Title, opt.NeedLg);
        if (opt.BodyObj != null) {
            modalObj.Body.append(opt.BodyObj);
        }
        if (opt.FootObj != null) {
            modalObj.Foot.append(opt.FootObj);
        }
        return modalObj.Modal;
    },

    BuildContent: function (data) {
        var d = $.extend({}, ModalTool.dataFormat, data);
        switch (d.type) {
            case "hide": return '<input type="hidden" name="' + d.name + '" value="' + d.val + '" iFormat="' + d.iFormat + '" Verify=' + d.verify + ' ">';
            default: return '<div class="input-group"><span class="input-group-addon" >' + d.text + '</span ><input type="' + d.type + '" name="'
                + d.name + '" class="form-control ' + d.class + '" ' + d.attrs + ' value="' + d.val + '" iFormat="' + d.iFormat + '"  Verify=' + d.verify + ' /></div >';
        }
    },

    VerifyVal: function (e) {
        var form = $(e);
        var result = true;
        $(form).find('[Verify=true]').each(function () {
            var v = $(this).val();
            if (typeof (v) == 'undefined' || v == null || v.length == 0) {
                $(this).addClass('hightLight');
                result = false;
            }
        });
        if (!result) {
            BTUTILS.ShowMessage({ title: "请输入数据！", type: "warning" });
        }
        return result;
    },

    FormSubmit(e) {
        if (!this.VerifyVal(e)) {
            return;
        }

        var form = $(e);
        var formId = form.attr("id");
        formId = formId.substring(0, formId.length - 5);

        $(document.body).loading();
        var options = {
            url: form.attr('action'),
            success: function (datas, status, xhr, $form) {
                BTUTILS.ShowMessageByResult(JSON.parse(datas));
                ModalTool.OnSuccess(formId + "_Modal");
                //var parentObj = $(window.parent.document);
                //var searchButtonObj = parentObj.find('.panel-default div.active div.search-tool button.btn-info');
                //if (searchButtonObj[0] === undefined) { searchButtonObj = parentObj.find('.panel-default div.search-tool button.btn-info'); }
                //setTimeout(function () {
                //    searchButtonObj.trigger('click');
                //    parentObj.find("#edit_model a span").trigger('click');
                //}, 300);
            },
            error: function (xhr, status, error, $form) {
                BTUTILS.ShowMessage({ title: error, type: "warning" });
                //console.log("error", xhr, status, error, $form)
            },
            complete: function (xhr, status, $form) {
                $(document.body).loading('hide');
                //console.log("complete", xhr, status, $form)
            }
        };
        form.ajaxSubmit(options);
    },

    AjaxSubmit: function (e) {
        if (!this.VerifyVal(e)) {
            return;
        }

        var form = $(e);
        var formId = form.attr("id");
        formId = formId.substring(0, formId.length - 5);

        var data = '{';
        form.find('[Verify]').each(function () {
            var f = $(this).attr('iFormat');
            var n = $(this).attr('name');
            var verify = $(this).attr('verify');
            var v = $(this).val();

            switch (f) {
                case "num": data += '"' + n + '": ' + v + ','; break;
                case "str": data += '"' + n + '": "' + v + '",'; break;
            }
        });

        data = data.substring(0, data.length - 1) + "}";

        $(document.body).loading();
        $.ajax({
            url: form.attr('action'),
            type: "POST",
            dataType: "json",
            data: BuildJsonData(JSON.parse(data)),
            cache: false,
            success: function (result) {
                BTUTILS.ShowMessageByResult(result);
                ModalTool.OnSuccess(formId + "_Modal");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                var text = errorThrown == "" ? textStatus : errorThrown;
                BTUTILS.ShowMessage({ title: text, type: "warning" });
            },
            complete: function (xhr, status, $form) {
                $(document.body).loading('hide');
                //console.log("complete", xhr, status, $form)
            }
        });
    },

    OnSuccess: function (formId) {
        if (ModalTool.closable) {
            $("#" + formId).modal('hide');
        }

        if (ModalTool.Callback_Success != null) {
            ModalTool.Callback_Success();
        }
    },
}

var modalSubmit = function (id, submitType) {
    var form = $('#' + id + '_Form');
    if (form.length > 0) {
        switch (submitType) {
            case SubmitTypeEnum.Form: ModalTool.FormSubmit(form); break;
            case SubmitTypeEnum.Ajax: ModalTool.AjaxSubmit(form); break;
        }
    }
}

var spanAlignTool = {
    spanDefult: {
        obj: $('form#formal_edit span.input-group-addon'),
        num: null
    },

    setSpanWidth: function (opt) {
        if (!opt) opt = this.spanDefult;
        if (!opt.num) {
            var max = 0;
            opt.obj.each(function () {
                var _this = $(this);
                if (typeof (_this.attr('span_isAlign')) == 'undefined') {
                    var len = _this.html().trim().replace('：', '').replace(':', '').length;
                    if (len > max) max = len;
                }
            });
            opt.num = max;
        }

        opt.obj.each(function () {
            var _this = $(this);
            if (typeof (_this.attr('span_isAlign')) == 'undefined') {
                var lable = _this.html().trim().replace('：', '').replace(':', '');
                var len = lable.length;
                var newlable = "";
                if (len < opt.num && len !== 1) {
                    var c = opt.num - len;
                    var width = c / (len - 1);
                    for (i = 0; i < lable.length; i++) {
                        var element = lable.charAt(i);
                        if (i !== 0) { newlable += "<span style='padding-left: " + width + "em'></span>"; }
                        newlable += element;
                    }
                } else {
                    newlable = lable;
                }
                _this.html(newlable + "：");
                _this.attr('span_isAlign', true);
            }
        });
    },

    setSpanLeft: function (opt) {
        if (!opt) opt = spanDefult;
        if (!opt.num) {
            var max = 0;
            opt.obj.each(function () {
                var _this = $(this);
                if (typeof (_this.attr('span_isAlign')) == 'undefined') {
                    var len = _this.html().trim().replace('：', '').replace(':', '').length;
                    if (len > max) max = len;
                }
            });
            opt.num = max;
        }

        opt.obj.each(function () {
            var _this = $(this);
            if (typeof (_this.attr('span_isAlign')) == 'undefined') {
                var lable = _this.html().trim().replace('：', '').replace(':', '');
                var len = lable.length;
                var newlable = "";
                if (len < opt.num && len !== 1) {
                    var c = opt.num - len;
                    newlable += lable + "：" + "<span style='padding-left: " + c + "em'></span>";
                } else {
                    newlable = lable + "：";
                }
            }
            _this.html(newlable);
            _this.attr('span_isAlign', true);
        });
    }
}