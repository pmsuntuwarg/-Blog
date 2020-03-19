$(function () {
    var pageListDT = $("#mediaListDT").DataTable({
        processing: true,
        serverSide: true,
        ajax      : {
            url     : "/admin/media/getMedias",
            type    : "post",
            dataType: "json"
        },
        language: {
            search           : "",
            searchPlaceholder: "Search.."
        },
        columns: [
            {
                data: "previewUrl",
                name: "File Preview",
                render: (data, type, row, meta) => `<img src=${data} />`
            },
            { data: "fileName", name: "File Name", searchable: false, autoWidth: true },
            {
                data  : "id",
                render: (data, type, row, meta) => `<a href="Media/Detail/${data}"> Detail </a>`
            }
        ]
    });
});
