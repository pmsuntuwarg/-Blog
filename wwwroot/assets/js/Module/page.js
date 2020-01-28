$(function () {
    var pageListDT = $("#pageListDT").DataTable({
        processing: true,
        serverSide: true,
        ajax      : {
            url     : "/admin/page/getpages",
            type    : "post",
            dataType: "json"
        },
        language: {
            search           : "",
            searchPlaceholder: "Search.."
        },
        columns: [
            { data: "pageName", name: "Page Name", searchable: false, autoWidth: true },
            { data: "title", name: "Page Title", searchable: false, autoWidth: true },
            {
                data  : "id",
                render: (data, type, row, meta) => `<a href="Page/Detail/${data}"> Detail </a> <a href="Page/Update/${data}"> Edit </a>`
            }
        ]
    });
});
