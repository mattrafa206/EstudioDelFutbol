using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using EstudioDelFutbol.Common.Utils;
using EstudioDelFutbol.Logger;

namespace EstudioDelFutbol.Common.Security
{
  public class Security
  {
	  public static byte[] Ping(BizServer oBizServer, XmlElement objParams)
	  {
		  return new byte[] { (byte)'O', (byte)'K' };
	  }

	  public static bool TrustAllCertificateCallback(object sender,
		X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
	  {
		  //Return True to force the certificate to be accepted.
		  return true;
	  } 
  }
}
