function moveButton(elem) {
  var id=  elem.getAttribute("data-Id")
    if ($(elem).parent().attr("id") == "nonSelectedT") {

        if (document.getElementById('selected').innerHTML.indexOf(id) != -1)
        {
          
        }
        else
        {
            $(elem).detach().appendTo('#selected');
        }

      
    }
    else if ($(elem).parent().attr("id") == "nonSelected")
    {
        if (document.getElementById('selected').innerHTML.indexOf(id) != -1)
        {
            document.getElementById('small-error').innerHTML += data + " finns inte";
            document.getElementById('small-error').style.color = "#333333";
        }
        else
        {
            $(elem).detach().appendTo('#selected');
        }
        
       
      
    }

    else
    {
        if (document.getElementById('selected').innerHTML.indexOf(id) != -1) {
            $(elem).detach();
            $('#nonSelected').prepend(elem);
        }
        else
        {

        
        $(elem).detach();
      
        }
    }
}
$("#SearchText").keyup(function (event) {
    if (event.keyCode == 13) {
        $("#addtoNote").click();
    }
});

function copy() {
    var elem = document.getElementById('btnDanger');
    var id = elem.getAttribute("data-Id");
    var data = document.getElementById('SearchText').value;

    if (document.getElementById('selected').innerHTML.indexOf(data) != -1) 
    {
        document.getElementById('SearchText').value = '';
        alert("Du har redan " + data + " i din lista. Försök med annan.")

    }
    else if (document.getElementById('nonSelected').innerHTML.indexOf(data) != -1)
    {
        document.getElementById('SearchText').value = '';
       $('*[data-Id="' + data + '"]').detach().appendTo('#selected');     
    }
   else {
       var oldhtml;
       var newhtml = data + " finns inte";
       $('#small-error').fadeOut(500, function ()
       {
           $('#small-error').html(newhtml).fadeIn();
           $('#small-error').delay(2000);
           $('#small-error').fadeIn(500, function () { $('#small-error').html(oldhtml); });
       });

  
       
       document.getElementById('SearchText').value = '';
   } //document.getElementById('selected').innerHTML += "<button id='btnDanger' onclick='moveButton(this)' type='button' class='btn-danger ingrediens-item'>" + data + "</button><br/>";
    
    
}
function clearForm()
{
    var elem = document.getElementById('btnDanger');
    var id = elem.getAttribute("data-Id");
    var div = document.getElementById('selected').value='';
    while (document.getElementById('selected').innerHTML.indexOf(elem) != -1) {
        if ($("#selected").find("#btnDanger").length > 0) {
            $(elem).detach();
            $('#nonSelected').prepend(elem);
        }
        else {
            
        }
    }
}
       



$(document).ready(function () {
 
    $("#SearchText").autocomplete({
        autoFocus: true,
        minLength: 0,
        scroll: true,
          source: function (request, response) {
              $.ajax({
                  url: "/Search/AutoCompleteIngrediens",
                  type: "POST",
                  dataType: "json",
                  data: { term: request.term },
                  success: function (data) {
                      if (data.length > -1) {
                          response($.map(data, function (item) {
                              return {
                                  label: item.Name,
                                  val: item.Name
                              };
                          }))
                      } else {
                          response([{  label: item.Name, val:item.Name }]);
                      }
                  }
              });

          }
          })
      });
  

      

$(".Kött-open-button-1").click(function () {
    $("div.Kött-div-1").show("slow");

    $("div.Grönsaker-div-1").hide("slow");
    $("div.Fisk-div-1").hide("slow");
});

$(".Grönsaker-open-button-1").click(function () {
    $("div.Grönsaker-div-1").show("slow");

    $("div.Kött-div-1").hide("slow");
    $("div.Fisk-div-1").hide("slow");
});

$(".Fisk-open-button-1").click(function () {
    $("div.Fisk-div-1").show("slow");

    $("div.Grönsaker-div-1").hide("slow");
    $("div.Kött-div-1").hide("slow");
});

$(".Hide-1").click(function () {
    $(".div-1").hide("slow");
});

