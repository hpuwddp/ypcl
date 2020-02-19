var Product = {
    PageClick: function (pageIndex, pageSize) {

        pageSize = pageSize == undefined ? 10 : pageSize;

        //服务器端分页
        var param = {};
        param["pageIndex"] = pageIndex;
        param["pageSize"] = pageSize;
        param["ProductName"] = $('#ProductName').val();
        $.ajax({
            type: 'POST',
            url: '/Product/GetProductList',
            data: param,
            dataType: 'json',
            success: function (result) {

                var json = result;
                var Html = "";
                if (json.messCode == 200 && json.data.length > 0) {

                    $(json.data).each(function (i, item) {
                        Html += "<tr>";
                        Html += "<td><input type=\"checkbox\" name=\"IDCheck\" value=\"" + item.RowID + "\" class=\"acb\" /></td>";
                        Html += "<td>" + item.ProductName + "</td>";
                        Html += "<td>" + item.Days + "</td>";
                        Html += "<td>" + item.Price + "</td>";
                        Html += "<td>";
                        // Html += "<a class=\"edit\" href=\"/Sys/UserEdit?rowid=" + item.RowID + "\" title=\"编辑\">编辑</a>";
                        Html += "<a  href=\"javascript:void(0)\" onclick=\"Product.Edit('" + item.RowID + "')\" title=\"编辑\">编辑</a>";
                        Html += "<a  href=\"javascript:void(0)\" onclick=\"Product.Del('" + item.RowID + "')\" title=\"删除\">删除</a>";
                        Html += "</td>";
                        Html += "</tr>";
                    });
                }

                $("#tabInfo tbody").html(Html);

                //加载分页控件信息
                $(".pageSpan").createPage({
                    pageIndex: pageIndex,
                    totalPage: GetPageCount(parseInt(json.recordCount), parseInt(pageSize)),
                    pageSize: pageSize,
                    backFn: Product.PageClick
                });
            },
            error: function (data) {
                console.log(data.msg);
            },
        });
    },
    Edit: function (rowid) {
        $("#showTips").fancybox({
            'href': '/Product/ProductEdit?rowid=' + rowid,
            'width': 733,
            'height': 530,
            'type': 'iframe',
            'hideOnOverlayClick': false,
            'showCloseButton': false,
            'onClosed': function () {
                window.location.href = '/Product/ProductList';
            }
        });
        $("#showTips").click();
    },
    Del: function (rowid) {
        if (confirm('确定要删除吗') == true) {
            var param = {};
            param["rowid"] = rowid;
            param["batch"] = false;
            $.ajax({
                url: "/Product/DelProduct", type: "post", data: param, success: function (result) {
                    if (result.msgCode == "200") {
                        // $.jBox.tip("删除成功", "success");
                        Product.PageClick(1);
                    } else {
                        $.jBox.tip("删除失败", "error");
                    }
                }
            });
            return true;

        } else {

            return false;

        }
    },
    Export: function () {
        var param = {};
        param["DepotName"] = "";

        $.ajax({
            url: "/Store/ToExcel", type: "post", data: param, success: function (result) {
                if (result.Path != undefined && result.Path != "") {
                    var path = unescape(result.Path);
                    window.location.href = path;
                    return true;
                } else {
                    $.jBox.info(result.d, "提示");
                    return true;
                }
            }
        });
        return true;
    },
    SelectAll: function (item) {
        var flag = $(item).attr("checked");
        if (flag || flag == "checked") {
            $("input[name='Store_item']").attr("checked", true);
        }
        else {
            $("input[name='Store_item']").attr("checked", false);
        }
    },
    BatchDel: function () {

        var chklist = $("#tabInfo tbody tr").find("input:checked");
        var ids = "";
        $.each(chklist, function (index, item) {
            ids += $(item).attr("value") + ",";
        });

        if (ids.length > 0) {
            var submit = function (v, h, f) {
                if (v == 'ok') {
                    var param = {};
                    param["batch"] = true;
                    param["rowid"] = ids;
                    $.ajax({
                        url: "/Store/DelStore", type: "post", data: param, success: function (result) {
                            if (result.msgCode == "200") {

                                Store.PageClick(1);
                            } else {
                                $.jBox.tip("删除失败", "error");
                            }
                        }
                    });
                }
            };
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        }
        else {
            $.jBox.tip("请至少选择一条数据!", 'info');
        }
    }
}


//获取总的页数信息
function GetPageCount(recordCount, pageSize) {
    if (recordCount <= 0) return 1;
    if (recordCount % pageSize == 0)
        return parseInt(recordCount / pageSize);
    else
        return parseInt(recordCount / pageSize) + 1;
}