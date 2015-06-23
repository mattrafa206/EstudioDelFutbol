<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>ERROR 500</title>


<link href="<%= Url.Content("~/Styles/Error.css")%>" rel="stylesheet">

</head>
<body>
    <div>
      <div class="cont_error">
		<svg version="1.1" x="0px" y="0px"
        width="82px" height="82px" viewBox="405.5 405.5 82 82" enable-background="new 405.5 405.5 82 82" xml:space="preserve">
        <path id="error-5-icon" fill="#C20D0D" d="M463.483,405.5h-33.965L405.5,429.518v33.965l24.018,24.018h33.965l24.018-24.018v-33.965
        L463.483,405.5z M460.089,467.479l-13.39-13.39l-13.39,13.391l-7.192-7.195l13.388-13.389l-13.389-13.388l7.196-7.192l13.386,13.385
        l13.384-13.387l7.198,7.193l-13.387,13.387l13.389,13.387L460.089,467.479z"/>
        </svg>
        <h1>500 / SERVER ERROR</h1>
        <h3>ERROR INTERNO DE SERVIDOR</h3>
        <h5>No se puede obtener lo solicitado, debido a problemas internos de configuración del servidor.</h5>
        <a href="#" onclick="javascript:history.back(); return false;">Volver</a>
	  </div>
    </div>
</body>
</html>
