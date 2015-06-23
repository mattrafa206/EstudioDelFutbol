<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<EstudioDelFutbol.Models.LoginModel>"
    MasterPageFile="~/Views/Shared/SiteMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Login
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .validation-summary-errors
        {
            text-align: center;
            margin-top: -20px;
        }
        .validation-summary-errors ul
        {
            margin: 0px;
            padding: 0px;
            list-style: none;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            navigatorMenu('Analysis');
            <% if(User.Identity.IsAuthenticated) { %>   window.location.replace('<%= @Url.Action("Analysis", "Pages")%>'); <%}%> 

            if (localStorage.getItem("UserName") != undefined) {
                    document.getElementById("UserName").value = localStorage.getItem("UserName");
            }
        });

        function setRememberMe() {
            localStorage.setItem("UserName", document.getElementById("UserName").value);
        }

    </script>

    <!-- home bg y contenido -->
    <section class="bg_foto_home">
        <div class="container">
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6 login">
                    <div class="well">
                        <div class="row">
                            <div class="col-sm-3">
                            </div>
                            <% using (Html.BeginForm())
                            { %>
                            <div class="col-sm-6">
                                <h3>
                                    INGRESAR</h3>
                                <h5>
                                    Nombre de usuario</h5>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-user" aria-hidden="true"></span>
                                    </div>
                                     <input id="UserName" name="UserName" type="text" class="form-control" placeholder="Usuario"  maxlength="50" />
                                </div>
                                <h5>
                                    Password</h5>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-lock" aria-hidden="true"></span>
                                    </div>
                                   <input id="Password" name="Password" type="password" class="form-control" placeholder="Contraseña"  maxlength="50" />
                                </div>
                                <button class="btn btn-success btn-block"  type="submit" onclick="setRememberMe();">
                                    INGRESAR</button>
                            </div>
                             <% } %>
                            <!-- /.col-sm-8 -->
                            <div class="col-sm-3">
                            </div>
                        </div>
                        <!-- /.row -->
                        <div class="row">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-10">
                                <!-- MENSAJE DE ERROR LOGIN -->
                                <% if (ViewData.ModelState.Any((x => x.Value.Errors.Any())))
                                   { %>
                                <div class="alert alert-danger alert-dismissible" role="alert" style="margin-bottom: 10px;">
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <span aria-hidden="true">&times;</span></button>
                                    <strong><span class="glyphicon glyphicon-warning-sign" aria-hidden="true"></span> Error!</strong><%: Html.ValidationSummary(true, "Usuario y/o contraseña incorrectos")%>
                                </div>
                                <% } %>
                                <!-- /MENSAJE DE ERROR LOGIN -->
                            </div>
                            <!-- /.col-md-10 -->
                            <div class="col-md-1">
                            </div>
                        </div>
                        <!-- /.row -->
                        <div class="row">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-10 tex_info">
                                <h4>
                                    <strong>¿No tiene cuenta?</strong></h4>
                                <p>
                                   Estudio del fútbol es un servicio por encargo, contáctenos y a la brevedad podremos informarlo sobre nuestros servicios para su cuerpo técnico. Consultar por el servicio <a style="cursor: pointer" onclick="window.location.href = '<%= @Url.Action("Contact", "Pages")%>'">ahora </a>.
<br />¿Olvidó su contraseña?, contáctenos para restablecer su cuenta y la brevedad nos pondremos en contacto. <a style="cursor: pointer" onclick="window.location.href = '<%= @Url.Action("Contact", "Pages")%>'">Olvide Contraseña.</a></p>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <!-- /.row -->
                    </div>
                    <!-- /.well -->
                   
                </div>
                <!-- /.conenido_home -->
                <div class="col-md-3">
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container -->
    </section>
</asp:Content>
