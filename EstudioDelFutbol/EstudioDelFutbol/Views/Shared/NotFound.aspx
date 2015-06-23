<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>ERROR 404</title>


<link href="<%= Url.Content("~/Styles/Error.css")%>" rel="stylesheet">

</head>
<body>
    <div>
      <div class="cont_error">
		<svg version="1.1" x="0px" y="0px" width="94.585px" height="82px" viewBox="398.167 405.5 94.585 82"
		enable-background="new 398.167 405.5 94.585 82">
		<path id="warning-6-icon" fill="#FF930D" d="M441.852,440.86h7.213v21.873h-7.213V440.86z M445.459,474.017
		c-2.15,0-3.893-1.744-3.893-3.894c0-2.148,1.742-3.892,3.893-3.892s3.891,1.743,3.891,3.892
		C449.35,472.272,447.61,474.017,445.459,474.017z M445.459,406.444l-46.252,80.111h92.504L445.459,406.444z M445.459,424.405
		l30.695,53.169h-61.391L445.459,424.405z"/>
		</svg>
        <h1 id="error404">404 / NOT FOUND</h1>
        <h3>PAGINA NO ENCONTRADA</h3>
        <h5>El servidor no encuentra lo solicitado, contenido inexistente.</h5>
        <a href="#" onclick="javascript:history.back(); return false;">Volver</a>
	  </div>
    </div>
</body>
</html>
