$(function () {
    var pageListDT = $("#pageListDT").DataTable({
        processing: true,
        serverSide: true,
        ajax: {
            url: "/admin/page/getpages",
            type: "post",
            success: (data) => {
                console.log(data);
            },
            error: (er) => {

            }
        },
        language: {
            search: "",
            searchPlaceholder : "Search.."
        },
        columns: [
            {data:"id", name:"Id", searchable:false,autoWidth:true, hidden:true},
            {data:"pageName", name:"Page Name", searchable:false,autoWidth:true},
            {data:"title", name:"Page Title", searchable:false,autoWidth:true}
        ]
    });
});