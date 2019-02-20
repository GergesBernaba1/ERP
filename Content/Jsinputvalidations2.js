
// call like this         <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" OnClientClick="Validations();" ValidationGroup="1" />

function Validations() {
    //var all = document.getElementsByTagName("*");
    all = document.forms[0].elements;
    for (var i = 0, max = all.length; i < max; i++)
    {
        if (all[i].type == 'text')
        {
            
            if (all[i].value.trim() == "")
            {
                all[i].style.borderColor = "Red";
            }
            else
            {

                all[i].style.borderColor = "#d6d4d4";//"Gainsboro";

            }
        }
        /////////////////////password//////////

        if (all[i].type == 'password') {

            if (all[i].value.trim() == "") {
                all[i].style.borderColor = "Red";
            }
            else {

                all[i].style.borderColor = "#d6d4d4";//"Gainsboro";

            }
        }

        /////////////////////password//////////

        if (all[i].type == 'email') {

            if (all[i].value.trim() == "") {
                all[i].style.borderColor = "Red";
            }
            else {

                all[i].style.borderColor = "#d6d4d4";//"Gainsboro";

            }


            if (all[i].type == 'number') {

                if (all[i].value.trim() == "") {
                    all[i].style.borderColor = "Red";
                }
                else {

                    all[i].style.borderColor = "#d6d4d4";//"Gainsboro";

                }
        }

        /////////////select-one

        if (all[i].type == 'select-one') {

            if (all[i].selectedIndex == 0) {
                all[i].style.borderColor = "Red";
            }
            else {

                all[i].style.borderColor = "#d6d4d4";//"Gainsboro";

            }
        }


        
    }



    
}

//   call like this     <asp:TextBox ID="TextBox1" runat="server" onchange="changeborderColorafterfill(this)"></asp:TextBox>

function changeborderColorafterfill(txt)
{

    if (txt.value.trim() != "")
    {
        txt.style.borderColor = "#d6d4d4";//"Gainsboro";
    }
    else
    {


        txt.style.borderColor = "Red";
    }
  
}


function changeborderColorafterfilldropdownlist(drp)

{
    if (drp.selectedIndex == 0)
    {
        drp.style.borderColor = "Red";

    }

    else
    {
        drp.style.borderColor = "#d6d4d4";

    }
    //var e = document.getElementById("ddlViewBy");
    //var strUser = e.options[e.selectedIndex].value;
}