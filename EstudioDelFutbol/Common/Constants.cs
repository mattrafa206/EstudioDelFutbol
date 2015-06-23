using System;
using System.Collections.Generic;
using System.Text;

namespace EstudioDelFutbol.Common
{
	public static class CoreConstants
	{

		public enum Gender
		{
			Male = 1,
			Female = 2
		}

		public enum AlertType
		{
			Panic = 1
		}

		public enum MessageState
		{
			Unread = 1,
			Read = 2,
			Deleted = 3,
			Sent = 4
		}

		public enum UserStatus
		{
			Online = 1,
			Offline = 2
		}


		public enum UserType
		{
			AppTaxi = 1,
			WidgetUser = 2,
			WidgetAdmin = 3,
			CompanyAdmin = 4,
			AppClient = 5
		}

		public enum DaysOfWeek
		{
			Sunday = 1,
			Monday = 2,
			Tuesday = 3,
			Wednesday = 4,
			Thursday = 5,
			Friday = 6,
			Saturday = 7
		}


		public enum PushEventType
		{
			Trips = 1,
			Messages = 2
		}

		public enum PushEventState
		{
			New = 1,
			Processed = 2
		}

		public enum PushEventUserState
		{
			Queued = 1,
			Sent = 2,
			SendError = 3,
			Received = 4
		}

		public enum TripsState
		{
			Pending = 1,
			Holding = 2,
			Inprogress = 3,
			Finished = 4,
			Cancel = 5,
			SystemFinished = 6,
			UnavailableTaxi = 7,
			ToExternal = 8
		}

		public enum TaxiState
		{
			Libre = 1,
			Ocupado = 2,
			EnCamino = 3,
			NoDisponible = 4,
			SinConexion = 5
		}

		public enum ExpiredAlerts
		{
			PolizaSeguro = 1,
			VerificacionTecnica = 2,
			Desinfeccion = 3,
			Licencia = 4,
			Licencia4 = 5
		}
	}
}