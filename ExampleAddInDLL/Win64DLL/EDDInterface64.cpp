#include "stdafx.h"  
#include "windows.h"  
#include "Objbase.h"
#include "EDDInterface64.h"
#include "stdio.h"
#include "strsafe.h"
#include <iostream>
#include <iomanip>
#include <fstream>

// STRINGs are in Unicode - this is a unicode DLL. 

using namespace std;

void WriteASCII(const char* str)		// ASCII file..
{
	fstream fstream("c:\\code\\eddif.txt", ios::app);         // open the file
	fstream << str;
	fstream.close();
}

void WriteUnicode(LPCTSTR buffer)
{
	char buffer2[30000];
	WideCharToMultiByte(CP_ACP, 0, buffer, -1, buffer2, sizeof(buffer2), 0, 0);		// need to convert back to ASCII
	WriteASCII((const char*)buffer2);
}

void WriteJournalEntry(JournalEntry ptr)
{
	int const arraysize = 30000;
	TCHAR buffer[arraysize];
	size_t cbDest = arraysize * sizeof(TCHAR);

	LPCTSTR pszFormat = TEXT("%s: %d:%s :'%s' '%s' '%s' : sys %s\nx%f y%f z%f | td%f ts%u | %d %d | loc '%s' st '%s' gm %s grp %s | %d cr\n");
	StringCbPrintfW(buffer, cbDest, pszFormat,
		ptr.utctime, ptr.indexno, ptr.eventid,
		ptr.name, ptr.info, ptr.detailedinfo,
		ptr.systemname,
		ptr.x, ptr.y, ptr.z, ptr.travelleddistance, ptr.travelledseconds, ptr.islanded ? 1 : 0, ptr.isdocked ? 1 : 0, ptr.whereami, ptr.shiptype, ptr.gamemode, ptr.group, ptr.credits);

	//int of = offsetof(NewJournalEntry, whereami);
	//StringCbPrintfW(buffer, cbDest, L"%d", of);

	if (ptr.materials.cDims == 1)
	{

		if ((ptr.materials.fFeatures & FADF_BSTR) == FADF_BSTR)
		{
			BSTR* bstrArray;
			HRESULT hr = SafeArrayAccessData(&ptr.materials, (void**)&bstrArray);

			long iMin = 0;
			SafeArrayGetLBound(&ptr.materials, 1, &iMin);
			long iMax = 0;
			SafeArrayGetUBound(&ptr.materials, 1, &iMax);

			for (long i = iMin; i <= iMax; ++i)
			{
				wcscat_s(buffer, arraysize, bstrArray[i]);
				wcscat_s(buffer, arraysize, L",");		// WORDs! length
			}
		}
	}
	wcscat_s(buffer, arraysize, L"\n");

	if (ptr.commodities.cDims == 1)
	{
		if ((ptr.commodities.fFeatures & FADF_BSTR) == FADF_BSTR)
		{
			BSTR* bstrArray;
			HRESULT hr = SafeArrayAccessData(&ptr.commodities, (void**)&bstrArray);

			long iMin = 0;
			SafeArrayGetLBound(&ptr.commodities, 1, &iMin);
			long iMax = 0;
			SafeArrayGetUBound(&ptr.commodities, 1, &iMax);

			for (long i = iMin; i <= iMax; ++i)
			{
				wcscat_s(buffer, arraysize, bstrArray[i]);		// WORDs! length
				wcscat_s(buffer, arraysize, L",");		// WORDs! length
			}
		}
	}

	wcscat_s(buffer, arraysize, L"\n");

	if (ptr.currentmissions.cDims == 1)
	{
		if ((ptr.currentmissions.fFeatures & FADF_BSTR) == FADF_BSTR)
		{
			BSTR* bstrArray;
			HRESULT hr = SafeArrayAccessData(&ptr.currentmissions, (void**)&bstrArray);

			long iMin = 0;
			SafeArrayGetLBound(&ptr.currentmissions, 1, &iMin);
			long iMax = 0;
			SafeArrayGetUBound(&ptr.currentmissions, 1, &iMax);

			for (long i = iMin; i <= iMax; ++i)
			{
				wcscat_s(buffer, arraysize, bstrArray[i]);		// WORDs! length
				wcscat_s(buffer, arraysize, L",");		// WORDs! length
			}
		}
	}

	WriteASCII("--------------- Journal Entry Data\n");

	WriteUnicode(buffer);
	WriteASCII("\n---------------\n");
}

EDDCallBacks callbacks;

EDD_API BSTR EDDInitialise(BSTR ver, BSTR folder, EDDCallBacks pcallbacks)
{
	WriteASCII("Initialise:");
	WriteUnicode(ver);
	WriteASCII(",");
	WriteUnicode(folder);
	WriteASCII("\n");
	callbacks = pcallbacks;
	//MessageBoxW(0, ver, L"Caption ɑːkɒn", MB_OK);
	return SysAllocString(L"Return String + ɑːkɒn");
}

EDD_API void EDDRefresh(BSTR Commander, JournalEntry ptr)
{
	TCHAR buffer[1000];
	wcscpy_s(buffer, 1000, L"Refresh:");
	wcscat_s(buffer, 1000, Commander);
	WriteUnicode(buffer);
	WriteJournalEntry(ptr);
	//MessageBoxW(0, Commander, L"Refresh", MB_OK);
}


EDD_API void EDDNewJournalEntry(JournalEntry ptr)
{

	WriteJournalEntry(ptr);

	SAFEARRAY sa;

	JournalEntry je = { 1,1,NULL,NULL,NULL,NULL,sa,sa, NULL,0,0,0, 0,0, 0,0, NULL,NULL,NULL,NULL, 0, NULL, sa };

	Fred fr = { 1,NULL,sa };

	WriteASCII("call:");
	(*callbacks.historycallback)(22, true, &je);

	const int arraysize = 1000;
	TCHAR buffer[arraysize];
	//*buffer = 0;
	//{
	//	BSTR* bstrArray;
	//	HRESULT hr = SafeArrayAccessData(&fr.currentmissions, (void**)&bstrArray);

	//	long iMin = 0;
	//	SafeArrayGetLBound(&fr.currentmissions, 1, &iMin);
	//	long iMax = 0;
	//	SafeArrayGetUBound(&fr.currentmissions, 1, &iMax);

	//	//StringCbPrintfW(buffer, sizeof(buffer), L"Res %d %d", iMin, iMax);
	//	//WriteUnicode(buffer);

	//	for (long i = iMin; i <= iMax; ++i)
	//	{
	//		wcscat_s(buffer, arraysize, bstrArray[i]);		// WORDs! length
	//		wcscat_s(buffer, arraysize, L",");		// WORDs! length
	//	}
	//}

	//WriteUnicode(buffer);
	//StringCbPrintfW(buffer, sizeof(buffer), L"Res %d %s", fr.a, fr.f);

	WriteUnicode(je.utctime);
	WriteJournalEntry(je);
	WriteASCII("back:");

	//WriteUnicode(buffer);

	//MessageBoxW(0, buffer, L"N2018JE", MB_OK);
}

EDD_API void EDDRequestedHistory(JournalEntry ptr)	// use it don't copy it
{
	WriteUnicode(L"Callback history:");
	WriteJournalEntry(ptr);
}


EDD_API void EDDTerminate()
{
	WriteASCII("Terminate\n");
	//MessageBoxW(0, L"!", L"Terminate", MB_OK);
}

EDD_API BSTR EDDActionCommand(BSTR action, SAFEARRAY& args)		// should always return a string
{
	int const arraysize = 600;
	TCHAR buffer[arraysize];
	size_t cbDest = arraysize * sizeof(TCHAR);

	*buffer = 0;

	wcscat_s(buffer, arraysize, action);
	wcscat_s(buffer, arraysize, L":");

	if (args.cDims == 1)
	{
		if ((args.fFeatures & FADF_BSTR) == FADF_BSTR)
		{
			BSTR* bstrArray;
			HRESULT hr = SafeArrayAccessData(&args, (void**)&bstrArray);

			long iMin = 0;
			SafeArrayGetLBound(&args, 1, &iMin);
			long iMax = 0;
			SafeArrayGetUBound(&args, 1, &iMax);

			for (long i = iMin; i <= iMax; ++i)
			{
				wcscat_s(buffer, arraysize, bstrArray[i]);
			}
		}
	}

	char buffer2[1000];

	WideCharToMultiByte(CP_ACP, 0, buffer, -1, buffer2, sizeof(buffer2), 0, 0);		// need to convert back to ASCII

	WriteASCII("Action Command: ");
	WriteASCII(buffer2);
	WriteASCII("\n");

	return SysAllocString(L"DLL Return value");
}

EDD_API void EDDActionJournalEntry(JournalEntry ptr)
{
	TCHAR buffer[1000];
	wcscpy_s(buffer, 1000, L"Journal Entry sent:");
	WriteUnicode(buffer);
	WriteJournalEntry(ptr);
}

