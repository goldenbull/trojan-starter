#include <iostream>
#include <Windows.h>
#include <Psapi.h>
#include <tchar.h>

void PrintProcessNameAndID(DWORD processID)
{
	TCHAR szProcessName[MAX_PATH] = TEXT("<unknown>");
	auto* const hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ,
	                                   FALSE,
	                                   processID);

	// Get the process name.
	if (nullptr != hProcess)
	{
		HMODULE hMod;
		DWORD cbNeeded;

		if (EnumProcessModules(hProcess, &hMod, sizeof(hMod), &cbNeeded))
		{
			GetModuleBaseName(hProcess, hMod, szProcessName,
			                  sizeof(szProcessName) / sizeof(TCHAR));
		}
	}

	// Print the process name and identifier.
	_tprintf(TEXT("%s  (PID: %u)\n"), szProcessName, processID);

	// Release the handle to the process.
	CloseHandle(hProcess);
}

int main()
{
	DWORD aProcesses[10240], cbNeeded;

	if (!EnumProcesses(aProcesses, sizeof(aProcesses), &cbNeeded))
	{
		return 1;
	}

	const auto cProcesses = cbNeeded / sizeof(DWORD);
	for (unsigned int i = 0; i < cProcesses; i++)
	{
		if (aProcesses[i] != 0)
		{
			PrintProcessNameAndID(aProcesses[i]);
		}
	}

	return 0;
}
