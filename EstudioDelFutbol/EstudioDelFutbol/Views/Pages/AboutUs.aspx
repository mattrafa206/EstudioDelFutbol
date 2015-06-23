<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Estudio del Fútbol
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script>


    jQuery(document).ready(function () {
        navigatorMenu('AboutUs');
    });

    </script>


    <section id="contact">
        <div class="container">
          
        </div>
    </section>

    </asp:Content>