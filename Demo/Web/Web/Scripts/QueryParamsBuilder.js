$.extend({
    BuildQueryParams: function (e) {
        var queryParams = new Array();

        $(e).find('input').each(function () {
            var obj = $.VerifyData(this);
            if (obj !== null) {
                queryParams.push(obj);
            }
        });

        $(e).find('select').each(function () {
            var obj = $.VerifyData(this);
            if (obj !== null) {
                queryParams.push(obj);
            }
        });

        return { "QueryParams": queryParams };
    },

    VerifyData: function (e) {
        var obj = {
            name: $(e).attr('name'),
            value: $(e).val(),
            group: $(e).attr('group'),
            mappingType: $(e).attr('mappingType'),
            nullable: $(e).attr('nullable')
        };
        if (obj.name && obj.name !== null && obj.name !== '') {
            if (obj.value) {
                if (obj.value !== null && obj.value !== '') {
                    return obj;
                }
            }
        }

        return null;
    }
})