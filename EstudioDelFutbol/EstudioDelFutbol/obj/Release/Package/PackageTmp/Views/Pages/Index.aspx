<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Estudio del Fútbol
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script>


    jQuery(document).ready(function () {

        //            <% if(!User.Identity.IsAuthenticated) { %>   window. location.replace('<%= Url.Content("~/Pages/Logout")%>'); <%} else {%> 


        //            <%}%> 
        navigatorMenu('Index');

    });



    </script>


     <!-- home bg y contenido -->
    <section class="bg_foto_home">
      <div class="container">
        <div class="row">
          <div class="col-md-2"></div>
          <div class="col-md-8 conenido_home">
            <h1>Estudio del Fútbol</h1>
            <h2>ANÁLISIS PRECISO PARA DECISIONES ACERTADAS</h2>
            <div class="cont_acceso">
              <button class="btn btn-default" onclick="window.location.href = '<%= Url.Content("~/Pages/Login")%>';" >INGRESAR</button>
            </div>
            <div class="row">
              <div class="col-md-1"></div>
              <div class="col-md-10">
                <h3>Una elección exitosa es el fruto de un buen análisis.</h3>
                <h4>Los datos precisos y documentados, son los que valen, y las estadísticas obtenidas de esto, no es más que la pura realidad. Por eso especializamos en análisis de futbol por encargo, al que le dedicamos toda nuestra pasión en búsqueda de esos detalles que no se ven a primera vista. Porque sabemos que las conclusiones despojadas de toda subjetividad son materia prima de suma importancia en la decisiones de los planteles técnicos.</h4>
              </div>
              <div class="col-md-1"></div>
            </div>
          </div><!-- /.conenido_home -->
          <div class="col-md-2"></div>
        </div><!-- /.row -->
      </div><!-- /.container -->
    </section>

    </asp:Content>