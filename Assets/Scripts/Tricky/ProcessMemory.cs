using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class ProcessMemory
{
    const int PROCESS_WM_READ = 0x0010;
    const int PROCESS_VM_WRITE = 0x0020;
    const int PROCESS_VM_OPERATION = 0x0008;
    const int FULLACCESS = 0x1F0FFF;
    Process process;
    IntPtr processHandle;

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(int hProcess,
      long lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteProcessMemory(int hProcess, long lpBaseAddress,
      byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);


    public bool InitaliseProcess(string ProcessName)
    {
        try
        {
            process = Process.GetProcessesByName(ProcessName)[0];
            processHandle = OpenProcess(FULLACCESS, false, process.Id);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public byte[] ReadBytes(long dataPos, int length)
    {
        int bytesRead = 0;
        byte[] buffer = new byte[length];
        ReadProcessMemory((int)processHandle, dataPos, buffer, buffer.Length, ref bytesRead);
        return buffer;
    }

    public float[] ReadFloats(long dataPos, int length)
    {
        int bytesRead = 0;
        byte[] buffer = new byte[length*4];
        ReadProcessMemory((int)processHandle, dataPos, buffer, buffer.Length, ref bytesRead);
        float[] result = new float[length];
        for (int i = 0; i < length; i++)
        {
            byte[] Buffer2 = new byte[4];
            Buffer2[0] = buffer[0+i * 4];
            Buffer2[1] = buffer[1+i * 4];
            Buffer2[2] = buffer[2+i * 4];
            Buffer2[3] = buffer[3+i * 4];

            result[i] = BitConverter.ToSingle(Buffer2, 0);
        }
        return result;
    }

    public void WriteFloat(long dataPos, float Input)
    {
        int bytesWritten = 0;
        byte[] buffer = BitConverter.GetBytes(Input);
        WriteProcessMemory((int)processHandle, dataPos, buffer, buffer.Length, ref bytesWritten);
    }

    public void Start()
    {

        int bytesRead = 0;
        byte[] buffer = new byte[24]; //'Hello World!' takes 12*2 bytes because of Unicode 


        // 0x0046A3B8 is the address where I found the string, replace it with what you found
        ReadProcessMemory((int)processHandle, 0x220B2733D70, buffer, buffer.Length, ref bytesRead);

        UnityEngine.Debug.Log(Encoding.Unicode.GetString(buffer) +
           " (" + bytesRead.ToString() + "bytes)");


        int bytesWritten = 0;
        buffer = Encoding.Unicode.GetBytes("It works!\0");
        // '\0' marks the end of string

        // replace 0x0046A3B8 with your address
        WriteProcessMemory((int)processHandle, 0x220B2733D70, buffer, buffer.Length, ref bytesWritten);
        UnityEngine.Debug.Log("Written!");
    }
}