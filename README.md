UCONN Microsoft Healthvault & OpenEMR Server
=================

The Microsoft Healthvault (MSHV) server at the University of Connecticut is a middle layer between mobile devices and two different EMR technologies (Microsoft Healthvault & OpenEMR). The main functionality of the server is to:

	- Provide mobile user authentication to MSHV.
	- Add/Get/Update/Delete Health Record Items (HRI) from MSHV.
	- Provide a middle layer to a locally running copy of OpenEMR.
 
End Points

	HRIs supported are listed at the server’s help page http://cicats9.engr.uconn.edu:14080/Help

Example Requests (GET/POST/UPDATE)

	/api/Health/GetAllergies
	/api/Health/AddAllergy
	/api/Health/UpdateAllergy
	/api/OpenEMR/GetPrescriptions

OpenEMR Info

Currently there is only one endpoint for OpenEMR (getprescriptions). This one endpoint is simply just a MySQL query to your locally running OpenEMR installation (see OpenEmrDal.cs).

Authentication

	To make requests to the MSHV server you will need a PublicId and a RecordId. These two Ids are for authentication purposes only. You can get personal Ids by visiting the following link and signing in to MSHV:
	http://cicats9.engr.uconn.edu:14080/Default.aspx

	Append them as a postfix to every request: ex - /api/Health/UpdateAllergy?PublicId=xxxx-xxxx-xxxx-xxxxx&RecordId=xxxx-xxxx-xxxx-xxxxx

Development Environment

	You will need Visual Studio 2012 or better (express is fine). To get the server running locally there are some prequisites (namely just install the SDK and setup the license file)

	http://msdn.microsoft.com/en-us/healthvault/bb802509.aspx#_Development_Environment

	After installing the Microsoft Healthvault SDK locally, rgister an app 'license key' to your local machine. Note, the license file is already created and can be found here: /~UconnHealthLicense/UconnHealth.pfx'.

Example JSON Post Requests (body of request) request type: 'application/json'

	Blood Pressure:

	{
	    "Systolic": "2000",
	    "Diastolic": "20",
	    "Pulse": "30",
	    "When": "2013-01-30T00:00:00",
	    "IrregularPulse": "true"
	}

	Condition:

	{
	    "Name": "Back Pains.",
	    "StopReason": "Feeling better.",
	    "StartDate": "2013-02-02T00:00:00",
	    "StopDate": "2013-02-28T00:00:00"
	}

	DiabeticProfile:

	{
		"GlucoseUpperBound": "30",
		"GlucoseLowerBound": "10",
		"When": "2013-10-07T00:00:00"
	}

	Emotion:

	{
		"Mood": "3",
		"Stress": "3",
		"WellBeing": "3",
		"When": "2013-10-07T00:00:00"
	}

	Exercise:

	{
		"Title": "Rope jumping2",
		"Distance": "3218.688",
		"Duration": "20",
		"When": "2013-10-07T00:00:00"
	}

	Height:

	{
		"Value": "45",
		"When": "2013-10-07T00:00:00"
	}

	Weight:

	{
		"Value": "210",
		"When": "2013-10-07T00:00:00"
	}

	Medication:

	{
	    "Name": "Amoxicillin404D",
	    "Strength": "20",
	    "Dose": "2mg",
	    "HowTaken": "Take after dinner.",
	    "Frequency": "0.5 to 0.6 per day.",
	    "DatePrescribed": "2013-01-01T00:00:00",
	    "DateStarted": "2013-01-01T00:00:00",
	    "DateDiscontinued": "2013-01-30T00:00:00",
	    "PrescribedBy": "Dr. Adams",
	    "Note": "Feeling cold still" 
	}

	Vital:

	{
		"Title": "Blood Pressure",
		"Unit": "BPM",
		"Value": "30",
		"When": "2013-10-07T00:00:00"
	}

	Allergy:

	{
		
	}

	PeakFlow:

	{
		
	}