<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Analisis
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
            navigatorMenu('Analysis');
            <% if (!User.Identity.IsAuthenticated)
               { %>   window.location.replace('<%= Url.Content("~/Pages/Login")%>'); <%}
               else
               {%> 


           <%}%>

            GetOptionsAnalysis();
            GetOptionsMatches();
            GetOptionsPlayers();
        });

        function GetOptionsAnalysis() {
            $.post('<%= Url.Content("~/Pages/GetOptionsAnalysis") %>', {},
           function (data) {
               for (var i = 0; i < data.length; i++) {
                   var li = document.createElement("li");
                   var a = document.createElement("a");
                   a.innerHTML = data[i].opName;
                   a.setAttribute('href', '#' + data[i].opName);
                   a.setAttribute('onclick', 'LoadVideo("' + data[i].fullName + '");setDetails("' + data[i].opName + '","' + data[i].fileName + '","' + data[i].date + ' - File Size: ' + data[i].size + ' Mb.","' + data[i].fullName + '")');
                   li.appendChild(a);
                   document.getElementById("ddAnalysis").appendChild(li)
               }
           });
        }

        function GetOptionsMatches() {
            $.post('<%= Url.Content("~/Pages/GetOptionsMatches") %>', {},
            function (data) {
                for (var i = 0; i < data.length; i++) {
                    var li = document.createElement("li");
                    var a = document.createElement("a");
                    a.setAttribute('href', '#' + data[i].opName);
                    a.setAttribute('onclick', 'LoadVideo("' + data[i].fullName + '");setDetails("' + data[i].opName + '","' + data[i].fileName + '","' + data[i].date + ' - File Size: ' + data[i].size + ' Mb.","' + data[i].fullName + '")');
                    a.innerHTML = data[i].opName;
                    li.appendChild(a);
                    document.getElementById("ddMatches").appendChild(li)
                }
            });
        }

        function GetOptionsPlayers() {
            $.post('<%= Url.Content("~/Pages/GetOptionsPlayers") %>', {},
            function (data) {
                for (var i = 0; i < data.length; i++) {
                    var li = document.createElement("li");
                    var a = document.createElement("a");
                    a.setAttribute('href', '#' + data[i].opName);
                    a.setAttribute('onclick', 'LoadVideo("' + data[i].fullName + '");setDetails("' + data[i].opName + '","' + data[i].fileName + '","' + data[i].date + ' - File Size: ' + data[i].size + ' Mb.","' + data[i].fullName + '")');
                    a.innerHTML = data[i].opName;
                    li.appendChild(a);
                    document.getElementById("ddPlayers").appendChild(li)
                }
            });
       }

       function GetLastTrainings() {
           $.post('<%= Url.Content("~/Pages/GetLastTrainings") %>', {},
            function (data) {
                if (data.length == 0) {
                    ShowErrorMsg("No hay imagenes disponibles por el momento");
                    document.getElementById("contentGallery").style.display = "none";
                    document.getElementById("contentVideo").style.display = "";
                }
                for (var i = 0; i < data.length; i++) {
                    var img = document.createElement("img");
                    img.setAttribute('class', 'img-responsive');
                    img.setAttribute('src', data[i]);
                    var a = document.createElement("a");
                    a.setAttribute('class', 'thumbnail');
                    a.setAttribute('data-gallery', '');
                    a.setAttribute('href', data[i]);
                    var a1 = document.createElement("a");
                    a1.setAttribute('class', 'btn btn-success');
                    a1.setAttribute('download', '');
                    a1.setAttribute('href', data[i]);
                    a1.setAttribute('style', 'float: right;margin-bottom: 20px;');
                    a1.innerHTML = "<span class='glyphicon glyphicon-download-alt' aria-hidden='true'></span> Descargar";
                    var div = document.createElement("div");
                    div.setAttribute('class', 'col-lg-12 col-md-12 col-xs-12 thumb');
                    a.appendChild(img);
                    div.appendChild(a);
                    div.appendChild(a1);
                    document.getElementById("contentGallery").appendChild(div);
                }
                var imprimir = document.createElement("a");
                imprimir.setAttribute('class', 'btn btn-success');
                imprimir.setAttribute('href', "#");
                imprimir.setAttribute('onclick', '$(".thumbnail").printArea();');
                imprimir.innerHTML = "<span class='glyphicon glyphicon-download-alt' aria-hidden='true'></span> Imprimir";

                document.getElementById("contentGallery").appendChild(imprimir);
            });
       }

       function GetLastSystems() {
           $.post('<%= Url.Content("~/Pages/GetLastSystems") %>', {},
            function (data) {
                if (data.length == 0) {
                    ShowErrorMsg("No hay imagenes disponibles por el momento");
                    document.getElementById("contentGallery").style.display = "none";
                    document.getElementById("contentVideo").style.display = "";
                }
                for (var i = 0; i < data.length; i++) {
                    var img = document.createElement("img");
                    img.setAttribute('class', 'img-responsive');
                    img.setAttribute('src', data[i]);
                    var a = document.createElement("a");
                    a.setAttribute('class', 'thumbnail');
                    a.setAttribute('data-gallery', '');
                    a.setAttribute('href', data[i]);
                    var a1 = document.createElement("a");
                    a1.setAttribute('class', 'btn btn-success');
                    a1.setAttribute('download', '');
                    a1.setAttribute('href', data[i]);
                    a1.setAttribute('style', 'float: right;margin-bottom: 20px;');
                    a1.innerHTML = "<span class='glyphicon glyphicon-download-alt' aria-hidden='true'></span> Descargar";
                    var div = document.createElement("div");
                    div.setAttribute('class', 'col-lg-12 col-md-12 col-xs-12 thumb');
                    a.appendChild(img);
                    div.appendChild(a);
                    div.appendChild(a1);
                    document.getElementById("contentGallery").appendChild(div);
                }
                var imprimir = document.createElement("a");
                imprimir.setAttribute('class', 'btn btn-success');
                imprimir.setAttribute('href', "#");
                imprimir.setAttribute('onclick', '$(".thumbnail").printArea();');
                imprimir.innerHTML = "<span class='glyphicon glyphicon-download-alt' aria-hidden='true'></span> Imprimir";

                document.getElementById("contentGallery").appendChild(imprimir);
            });
       }

        function GetMoreStatistics() {
            $.post('<%= Url.Content("~/Pages/GetMoreStatistics") %>', {},
                    function (data) {
                        if (data.length == 0) {
                            ShowErrorMsg("No hay imagenes disponibles por el momento");
                            document.getElementById("contentGallery").style.display = "none";
                            document.getElementById("contentVideo").style.display = "";
                        }
                        for (var i = 0; i < data.length; i++) {
                            var img = document.createElement("img");
                            img.setAttribute('class', 'img-responsive');
                            img.setAttribute('src', data[i]);
                            var a = document.createElement("a");
                            a.setAttribute('class', 'thumbnail');
                            a.setAttribute('data-gallery', '');
                            a.setAttribute('href', data[i]);
                            var a1 = document.createElement("a");
                            a1.setAttribute('class', 'btn btn-success');
                            a1.setAttribute('download', '');
                            a1.setAttribute('href', data[i]);
                            a1.setAttribute('style', 'float: right;margin-bottom: 20px;');
                            a1.innerHTML = "<span class='glyphicon glyphicon-download-alt' aria-hidden='true'></span> Descargar";
                            var div = document.createElement("div");
                            div.setAttribute('class', 'col-lg-12 col-md-12 col-xs-12 thumb');
                            a.appendChild(img);
                            div.appendChild(a);
                            div.appendChild(a1);
                            document.getElementById("contentGallery").appendChild(div);
                        }

                        var imprimir = document.createElement("a");
                        imprimir.setAttribute('class', 'btn btn-success');
                        imprimir.setAttribute('href', "#");
                        imprimir.setAttribute('onclick', '$(".thumbnail").printArea();');
                        imprimir.innerHTML = "<span class='glyphicon glyphicon-download-alt' aria-hidden='true'></span> Imprimir";

                        document.getElementById("contentGallery").appendChild(imprimir);
                    });
               }

       function LoadVideo(videoURL) {
           document.getElementById("contentSugerencias").style.display = "none"; 
           document.getElementById("contentGallery").style.display = "none";
           document.getElementById("contentVideo").style.display = "";
           document.getElementById("videoDetails").style.display = "";
           if (videoURL == "null") {
               document.getElementById("videoDetails").style.display = "none";
               document.getElementById("video").pause();
               document.getElementById("source").setAttribute("src", "");
               document.getElementById("video").load();
               ShowErrorMsg("No hay video disponible por el momento");
               document.getElementById("video").setAttribute("poster", "<%= Url.Content("~/img/bg_edf_home.png") %>");
           }
           else {
               document.getElementById("video").setAttribute("poster", "");
               document.getElementById("source").setAttribute("src", videoURL);
               document.getElementById("video").load();
               document.getElementById("video").play();
           }
       }

        function LoadGalleryTrainings() {
            document.getElementById("contentSugerencias").style.display = "none"; 
           document.getElementById("contentGallery").innerHTML = "";
           document.getElementById("video").pause();
           document.getElementById("contentVideo").style.display = "none";
           document.getElementById("videoDetails").style.display = "none";
           document.getElementById("contentGallery").style.display = "inline";
           GetLastTrainings();
        }

        function LoadGallerySystems() {
            document.getElementById("contentSugerencias").style.display = "none";
            document.getElementById("contentGallery").innerHTML = "";
            document.getElementById("video").pause();
            document.getElementById("contentVideo").style.display = "none";
            document.getElementById("videoDetails").style.display = "none";
            document.getElementById("contentGallery").style.display = "inline";
            GetLastSystems();
        }

        function LoadGalleryStatistics() {
            document.getElementById("contentSugerencias").style.display = "none"; 
           document.getElementById("contentGallery").innerHTML = "";
           document.getElementById("video").pause();
           document.getElementById("contentVideo").style.display = "none";
           document.getElementById("videoDetails").style.display = "none";
           document.getElementById("contentGallery").style.display = "inline";
           GetMoreStatistics();
        }



       function setDetails(seccion, name, detail, link) {
           document.getElementById("SelectTitleOp").innerHTML = seccion.toUpperCase();
           document.getElementById("FileNameOp").innerHTML = " " + name;
           document.getElementById("FileDetailOp").innerHTML = detail;
           document.getElementById("downloadLink").setAttribute('href', link);
       }

       function LoadSugerencias(){
           document.getElementById("contentGallery").innerHTML = "";
           document.getElementById("video").pause();
           document.getElementById("contentVideo").style.display = "none";
           document.getElementById("videoDetails").style.display = "none";
           document.getElementById("contentSugerencias").style.display = ""; 
       }

       function SendMailContact() {
           var FullName = document.getElementById("clubName").innerHTML;
           var Email = document.getElementById("txtContactEmail").value.trim();
           var Body = document.getElementById("txtContactBody").value.trim();

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
               MsgError = MsgError + "Debe ingresar la sugerencia.<br />";
           }

           if (MsgError.trim() == "") {
               $.post('<%= Url.Content("~/Pages/SendMailContact") %>', { fullName: FullName, email: Email, subject: "", phone: "", clubName: FullName, body: Body, title: "Sugerencia" },
          function (data) {
              if (data == "Success") {

                  //TODO Ver si realmente se enviara el mail para validar la cuenta
                  document.getElementById("msg").className = "";
                  document.getElementById("msg").className = "modal-content text-success";
                  document.getElementById("msg").innerHTML = "Sugerencia enviada correctamente.<br />";

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
           document.getElementById("txtContactEmail").value = "";
           document.getElementById("txtContactBody").value = "";

           MsgError = "";
       }

    </script>
    <section class="header_reproductor">
        <div style="background-color: #303030; top: 52px; position: relative;">
            <div class="container">
                <div class="row">
                    <div class="col-md-4 logo_club">
                        <img src='<%: EstudioDelFutbol.CommonWeb.Utils.GetUserInfoFromTicket(HttpContext.Current.User.Identity).Picture %>'
                            width="59" height="59">
                        <h4 id="clubName"><%: EstudioDelFutbol.CommonWeb.Utils.GetUserInfoFromTicket(HttpContext.Current.User.Identity).Nombre %></h4>
                        <h5>
                            <i class="fa fa-futbol-o"></i>Próx. Rival:
                            <%: EstudioDelFutbol.CommonWeb.Utils.GetUserInfoFromTicket(HttpContext.Current.User.Identity).NextMatch%></h5>
                    </div>
                    <div class="col-md-8 menu">
                        <div class="btn-group unselectable" role="group" aria-label="...">
                            <div class="btn-group" role="group">
                                <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown"
                                    aria-expanded="false">
                                    <i class="fa fa-thumbs-up"></i><font class="hidden-xs"> ANALISIS </font><span class="caret">
                                    </span>
                                </button>
                                <ul id="ddAnalysis" class="dropdown-menu" role="menu">
                                </ul>
                            </div>
                            <div class="btn-group" role="group">
                                <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown"
                                    aria-expanded="false">
                                    <i class="fa fa-futbol-o"></i><font class="hidden-xs"> PARTIDOS </font><span class="caret">
                                    </span>
                                </button>
                                <ul id="ddMatches" class="dropdown-menu" role="menu">
                                </ul>
                            </div>
                            <div class="btn-group" role="group">
                                <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown"
                                    aria-expanded="false">
                                    <i class="fa fa-user"></i><font class="hidden-xs"> MEJORES JUGADORES </font><span
                                        class="caret"></span>
                                </button>
                                <ul id="ddPlayers" class="dropdown-menu" role="menu">
                                </ul>
                            </div>
                            <div class="btn-group" role="group">
                                <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown"
                                    aria-expanded="false">
                                    <i class="fa fa-line-chart"></i><font class="hidden-xs"> ESTADÍSTICAS </font><span
                                        class="caret"></span>
                                </button>
                                <ul class="dropdown-menu" role="menu">
                                    <li><a id="ddLastTrainings" onclick="LoadGalleryTrainings();" href="#Formaciones">Ultimas
                                        2 Formaciones</a></li>
                                    <li><a id="ddLastSystems" onclick="LoadGallerySystems();" href="#Sistemas">Ultimos 2
                                        Sistemas</a></li>
                                    <li><a id="ddMoreStatistics" onclick="LoadGalleryStatistics();" href="#Estadisticas">Mas Estadísticas</a></li>
                                </ul>
                            </div>
                            <button type="button" class="btn btn-default" onclick="LoadSugerencias();">
                                <i class="fa fa-envelope-o"></i><font class="hidden-xs"> SUGERENCIAS </font></button>
                        </div>
                        <!-- /.btn-group -->
                    </div>
                </div>
            </div>
            <!-- /.container -->
        </div>
        <!-- /contenedor contenidos centrales -->
        <section class="contenido_seccion">
            <div class="container">
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <!-- /.col-md-2 -->
                    <div class="col-md-12 cont_central">
                        <div class="row row-centered">
                            <div class="col-md-8 col-centered">
                                <div id="contentGallery" style="display: none">
                                </div>
                                <div id="contentVideo" align="center" class="embed-responsive embed-responsive-16by9">
                                    <video id="video" controls preload="auto" poster="<%= Url.Content("~/img/bg_edf_home.png") %>"
                                        class="embed-responsive-item">
                                        <source id="source" src="" type='video/mp4; codecs="avc1.42E01E, mp4a.40.2"' />
                                    </video>
                                </div>
            <div id="contentSugerencias" style="display: none">
          <div class="col-md-2"></div>
          <div class="col-md-8">
          	<div class="well contacto">
              <h3 style="  margin-top:4px; margin-bottom:15px;  font-size:16px;">Para sugerencias, complete el siguiente formulario</h3>
              <form class="form-horizontal" role="form">
                  <div class="form-group">
                      <div class="col-sm-12">
                          <input type="email" class="form-control"  id="txtContactEmail" placeholder="Email">
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
                              Enviar Sugerencia</button>
                      </div>
                  </div>
                </form>

            </div><!-- /.well -->
            
          </div><!-- /.conenido_home -->
          <div class="col-md-2"></div>
        </div><!-- /.row -->


                            </div>
                        </div>
                        <div id="videoDetails" style="display: none;" class="row">
                            <div class="col-md-8 pievideo_left">
                                <h5 id="SelectTitleOp">
                                </h5>
                                <h4>
                                    <i class="fa fa-file-video-o"></i><font id="FileNameOp"></font>
                                </h4>
                            </div>
                            <div class="col-md-4 pievideo_right">
                                <h6 id="FileDetailOp">
                                </h6>
                                <a id="downloadLink" download class="btn btn-success"><span class="glyphicon glyphicon-download-alt"
                                    aria-hidden="true"></span>Descargar </a>
                            </div>
                        </div>
                    </div>
                    <!-- /.col-md-8 -->
                    <div class="col-md-2">
                    </div>
                    <!-- /.col-md-2 -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.container -->
        </section>
        <!-- /contenedor contenidos centrales -->
        <!-- The Bootstrap Image Gallery lightbox, should be a child element of the document body -->
        <div id="blueimp-gallery" class="blueimp-gallery blueimp-gallery-controls">
            <!-- The container for the modal slides -->
            <div class="slides">
            </div>
            <!-- Controls for the borderless lightbox -->
            <h3 class="title">
            </h3>
            <a class="prev">‹</a> <a class="next">›</a> <a class="close">×</a> <a class="play-pause">
            </a>
            <ol class="indicator">
            </ol>
            <!-- The modal dialog, which will be used to wrap the lightbox content -->
            <div class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">
                            </h4>
                        </div>
                        <div class="modal-body next">
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default pull-left prev">
                                <i class="glyphicon glyphicon-chevron-left"></i>Previous
                            </button>
                            <button type="button" class="btn btn-primary next">
                                Next <i class="glyphicon glyphicon-chevron-right"></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
