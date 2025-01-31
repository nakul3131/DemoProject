
var searchQueryId = localStorage.getItem('htmltest');
var label = localStorage.getItem('htmltest1');
$("#search").val(label);
$.ajax({
    url: "/Home/Menus",
    data: "{ '_SearchQueryId': '" + searchQueryId + "'}",
    dataType: "json",
    type: "Post",
    contentType: "application/json; charset=utf-8",
    success: function (data) {

        var data1 = JSON.stringify(data)
        var parsedTest = JSON.parse(data1);

        staticPagination(parsedTest, {
            perPage: 10
        });
    }

});


$(function () {
    $("#search").autocomplete({
        source: function (request, response) {
            $.ajax({
                url:"/Home/Menusearch", 
                data: "{ '_inputString': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger;

                    response($.map(data, function (item) {
                        debugger;

                        return {
                            label: item.QueryText,
                            value: item.SearchQueryId,

                        }

                    }))

                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },

        highlightItem: true,
        multiple: true,
        multipleSeparator: " ",
        minLength: 3,

        select: function (event, ui) {
            debugger;
            event.preventDefault();
            $(this).val(ui.item.label);
            var selectedObj = ui.item.value;
            localStorage.setItem('htmltest', selectedObj);
            var searchQueryIds = localStorage.getItem('htmltest');

            $.ajax({
                url: "/Home/Menus",
                data: { "_searchQueryId": searchQueryIds },
                type: "POST",
                dataType: "json",
                success: function (data) {
                    debugger;

                    var data1 = JSON.stringify(data)
                    var parsedTest = JSON.parse(data1);

                    
                    staticPagination(parsedTest, {
                        perPage: 10

                    });

                }
            });


        },

    })
   

});


let _currentPage, _perPage, _showPage, _paginationLength, $staticPaginationList;
let $list = $('#static-pagination-list');
let $pagination = $('#static-pagination');

function staticPagination(data, userOptions, o) {
    $.each(data, function (index, value) {
        $staticPaginationList = value;
        return false;
    });

    if (userOptions.perPage != null) {
        _perPage = userOptions.perPage;
    } else {
        _perPage = 10;
    }

    if (userOptions.showPage != null) {
        _showPage = userOptions.showPage;
    } else {
        _showPage = 10;
    }

    _paginationLength = parseInt($staticPaginationList.length / _perPage) + 1;
    _staticPagination(1);
}

function _staticPagination(page) {
    debugger;

    if (page === 0 && _currentPage !== 1) {
        // (previous)
        _staticPagination(_currentPage - 1);
    } else if (page === _paginationLength + 1 && _currentPage !== _paginationLength + 1) {
        // (next)
        _staticPagination(_currentPage + 1);
    } else {
        // (normal select)
        _currentPage = page;

        $list.empty();
        for (let i = _currentPage * _perPage - _perPage; i < _currentPage * _perPage; i++) {
            debugger;
            try {
                var controllername = $staticPaginationList[i].NameOfController;
                var actionname = $staticPaginationList[i].NameOfActionMethod;

                $.ajax({
                    url:"/Home/GetRoute",
                    data: "{ '_actionname': '" + actionname + "', '_controllername': '" + controllername + "'}",
                    dataType: "json",
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        $list.append('' +
                            '<li>' +
                            '<div><h6><a href=' + data + ' style="font-family: "Times New Roman", Times, serif; font-size: 25px;" > ' + $staticPaginationList[i].ResultUrl + '</a ></h6 ></div > ' +
                            '<div><small><p class="text-gary text-justify" style="color:rgb(110, 110, 110);font-family:sans-serif;font-size: 13px;">' + $staticPaginationList[i].ShortDescription + '</p></small></div></li>');
                        sortListDir();  

                    }
                })
               

            } catch (exception) {
                break;
            }
        }
    }

    if (_paginationLength >= 1) {
        debugger;
        $pagination.empty();
        if (_currentPage === 1) {
            $pagination.append('<li class="page-item disabled">' +
                '<a class="page-link" href="#" onclick="_staticPagination(' + 0 + ');">' +
                '<' +
                '</a>' +
                '</li>');
        } else {
            $pagination.append('<li class="page-item">' +
                '<a class="page-link" href="#" onclick="_staticPagination(' + 0 + ');">' +
                '<' +
                '</a>' +
                '</li>');
        }

        for (let _page = 1; _page <= _paginationLength; _page++) {
            if (_page === _currentPage) {
                $pagination.append('<li class="page-item active">' +
                    '<a class="page-link" href="#" onclick="_staticPagination(' + _page + ');">' + _page + '</a>' +
                    '</li>');
            } else {
                $pagination.append('<li class="page-item">' +
                    '<a class="page-link" href="#" onclick="_staticPagination(' + _page + ');">' + _page + '</a>' +
                    '</li>');
            }
        }

        if (_currentPage === _paginationLength) {
            $pagination.append('<li class="page-item disabled">' +
                '<a class="page-link" href="#" onclick="_staticPagination(' + (_paginationLength + 1) + ');">' +
                '>' +
                '</a>' +
                '</li>');
        } else {
            $pagination.append('<li class="page-item">' +
                '<a class="page-link" href="#" onclick="_staticPagination(' + (_paginationLength + 1) + ');">' +
                '>' +
                '</a>' +
                '</li>');
        }
    }
}

function sortListDir() {
    var list, i, switching, b, shouldSwitch, dir, switchcount = 0;
    list = document.getElementById("static-pagination-list");
    switching = true;
    // Set the sorting direction to ascending:
    dir = "asc";
    // Make a loop that will continue until no switching has been done:
    while (switching) {
        // Start by saying: no switching is done:
        switching = false;
        b = list.getElementsByTagName("LI");
        // Loop through all list-items:
        for (i = 0; i < (b.length - 1); i++) {
            // Start by saying there should be no switching:
            shouldSwitch = false;
            /* Check if the next item should switch place with the current item,
            based on the sorting direction (asc or desc): */

            if (b[i].innerHTML.toLowerCase() > b[i + 1].innerHTML.toLowerCase()) {
                /* If next item is alphabetically lower than current item,
                mark as a switch and break the loop: */
                shouldSwitch = true;
                break;
            }

        }
        if (shouldSwitch) {
            /* If a switch has been marked, make the switch
            and mark that a switch has been done: */
            b[i].parentNode.insertBefore(b[i + 1], b[i]);
            switching = true;
            // Each time a switch is done, increase switchcount by 1:
            switchcount++;
        } else {
            /* If no switching has been done AND the direction is "asc",
            set the direction to "desc" and run the while loop again. */
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}
