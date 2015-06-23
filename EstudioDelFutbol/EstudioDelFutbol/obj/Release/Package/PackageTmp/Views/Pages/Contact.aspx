<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Estudio del Fútbol
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>

        var MsgError = "";
        var reEmail = /([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)/;
        var rePhoneNumber = /^[0-9]{1,30}$/;

        var lenght100 = 100;
        var lenght250 = 250;
        var lenght500 = 500;

        jQuery(document).ready(function () {
            navigatorMenu('Contact');
        });

        function SendMailContact() {
            var FullName = document.getElementById("txtContactFullName").value.trim();
            var Email = document.getElementById("txtContactEmail").value.trim();
            var Body = document.getElementById("txtContactBody").value.trim();
            var ClubName = document.getElementById("txtContactClubName").value.trim();
            var Phone = document.getElementById("txtContactPhone").value.trim();

            //FullName
            if (FullName != "") {
                if (FullName.lenght > lenght100)
                    MsgError = MsgError + "El Nombre y Apellido debe tener menos de " + lenght100 + " caracteres.<br />";
            }
            else {
                MsgError = MsgError + "Debe ingresar su Nombre y Apellido.<br />";
            }

            //Phone
            if (Phone != "") {
                if (!(rePhoneNumber.test(Phone))) {
                    MsgError = MsgError + "Debe ingresar un Teléfono Valido.<br />";
                }
            }
            else {
                MsgError = MsgError + "Debe ingresar su Teléfono.<br />";
            }

            //ClubName
            if (ClubName != "") {
                if (ClubName.lenght > lenght100)
                    MsgError = MsgError + "El Nombre del Club debe tener menos de " + lenght100 + " caracteres.<br />";
            }
            else {
                MsgError = MsgError + "Debe ingresar el nombre del Club.<br />";
            }

            //Email
            if (Email != "") {
                if (!(reEmail.test(Email))) {
                    MsgError = MsgError + "Debe ingresar un Email Valido.<br />";
                }
                if (Email.lenght > lenght250)
                    MsgError = MsgError + "El Email debe tener menos de " + lenght250 + " caracteres.<br />";
            }
            else {
                MsgError = MsgError + "Debe ingresar su Email.<br />";
            }

            //Body
            if (Body != "") {
                if (Body.lenght > lenght500)
                    MsgError = MsgError + "La consulta debe tener menos de " + lenght500 + " caracteres.<br />";
            }
            else {
                MsgError = MsgError + "Debe ingresar la consulta.<br />";
            }

            if (MsgError.trim() == "") {
                $.post('<%= Url.Content("~/Pages/SendMailContact") %>', { fullName: FullName, email: Email, subject: "", phone:Phone, clubName:ClubName, body: Body, title: "Consulta" },
               function (data) {
                   if (data == "Success") {

                       //TODO Ver si realmente se enviara el mail para validar la cuenta
                       document.getElementById("msg").className = "";
                       document.getElementById("msg").className = "modal-content text-success";
                       document.getElementById("msg").innerHTML = "Consulta enviada correctamente.<br />";

                       ClearContactForm();
                   }
                   else {
                       document.getElementById("msg").className = "";
                       document.getElementById("msg").className = "modal-content text-danger";
                       document.getElementById("msg").innerHTML = data.toString();
                       MsgError = "";
                   }
               });
            }
            else {
                document.getElementById("msg").className = "";
                document.getElementById("msg").className = "modal-content text-danger";
                document.getElementById("msg").innerHTML = MsgError;
                MsgError = "";
            }
        }

        function ClearContactForm() {
            document.getElementById("txtContactFullName").value = "";
            document.getElementById("txtContactEmail").value = "";
            document.getElementById("txtContactBody").value = "";
            document.getElementById("txtContactPhone").value = "";
            document.getElementById("txtContactClubName").value = "";
            MsgError = "";
        }
    </script>

        <!-- home bg y contenido -->
    <section class="bg_foto_home">
      <div class="container">
        <div class="row login">
          <div class="col-md-2"></div>
          <div class="col-md-8">
          	<div class="well contacto">
              <h3 style=" margin:0; font-size:16px;">Para contactarnos, complete el siguiente formulario</h3>
              <h4 style=" margin-top:4px; margin-bottom:15px; font-size:16px;">y a la brevedad nos pondremos en contacto.</h4>
              <form class="form-horizontal" role="form">
                  <div class="form-group">
                      <div class="col-sm-12">
                          <input type="text" class="form-control" id="txtContactFullName" placeholder="Nombre y Apellido">
                      </div>
                  </div>
                  <div class="form-group">
                      <div class="col-sm-12">
                          <input type="email" class="form-control"  id="txtContactEmail" placeholder="Email">
                      </div>
                  </div>
                  <div class="form-group">
                      <div class="col-sm-12">
                          <input type="text" class="form-control"  id="txtContactPhone" placeholder="Telefono">
                      </div>
                  </div>
                   <div class="form-group">
                      <div class="col-sm-12">
                          <input type="text" class="form-control"  id="txtContactClubName" placeholder="Nombre Club">
                      </div>
                  </div>
                  <div class="form-group">
                      <div class="col-sm-12">
                          <textarea style=" resize: none; " class="form-control" rows="5" id="txtContactBody"></textarea>
                      </div>
                  </div>
                  <div class="form-group">
                      <div class="col-sm-12">
                          <button type="button" class="btn btn-default" data-toggle="modal" data-target=".bs-message-modal-sm"
                              onclick="SendMailContact()">
                              Enviar Consulta</button>
                      </div>
                  </div>
                </form>

            </div><!-- /.well -->
            
          </div><!-- /.conenido_home -->
          <div class="col-md-2"></div>
        </div><!-- /.row -->
      </div><!-- /.container -->
      <!-- Messages Modal Small -->
        <div class="modal fade bs-message-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel"
            aria-hidden="true">
            <div class="modal-dialog modal-xs">
                <div class="modal-content" id="msg" style="padding:20px;">
                </div>
            </div>
        </div>
        <!-- END Message Modal -->
    </section>
</asp:Content>
