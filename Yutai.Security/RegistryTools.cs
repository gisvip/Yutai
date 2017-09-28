using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace Yutai.Security
{
    public class RegistryTools
    {
        internal const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;

        internal const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;

        internal const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;

        internal const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 65535;

        private static UIntPtr HKEY_CLASSES_ROOT;

        private static UIntPtr HKEY_CURRENT_USER;

        private static UIntPtr HKEY_LOCAL_MACHINE;

        private static UIntPtr HKEY_USERS;

        private static UIntPtr HKEY_CURRENT_CONFIG;

        static RegistryTools()
        {
            RegistryTools.old_acctor_mc();
        }

        public RegistryTools()
        {
        }

        public static string Get32BitRegistryKey(string string_0, string string_1, string string_2)
        {
            string str;
            try
            {
                RegistryKey registryKey = RegistryTools.TransferKeyName32(string_0);
                if (registryKey != null)
                {
                    string[] strArrays = string_1.Split(new char[] { '\\' });
                    int num = 0;
                    while (num < (int)strArrays.Length)
                    {
                        registryKey = registryKey.OpenSubKey(strArrays[num]);
                        if (registryKey == null)
                        {
                            str = "";
                            return str;
                        }
                        else
                        {
                            num++;
                        }
                    }
                    if (registryKey != null)
                    {
                        str = Convert.ToString(registryKey.GetValue(string_2));
                        return str;
                    }
                }
                else
                {
                    str = "";
                    return str;
                }
            }
            catch
            {
            }
            str = "";
            return str;
        }

        public static string Get64BitRegistryKey(string string_0, string string_1, string string_2)
        {
            string str;
            int num = 257;
            try
            {
                UIntPtr uIntPtr = RegistryTools.TransferKeyName(string_0);
                IntPtr zero = IntPtr.Zero;
                StringBuilder stringBuilder = new StringBuilder("".PadLeft(1024));
                uint num1 = 1024;
                uint num2 = 0;
                IntPtr intPtr = new IntPtr();
                if (RegistryTools.Wow64DisableWow64FsRedirection(ref intPtr))
                {
                    RegistryTools.RegOpenKeyEx(uIntPtr, string_1, 0, num, out zero);
                    RegistryTools.RegDisableReflectionKey(zero);
                    RegistryTools.RegQueryValueEx(zero, string_2, 0, out num2, stringBuilder, ref num1);
                    RegistryTools.RegEnableReflectionKey(zero);
                }
                RegistryTools.Wow64RevertWow64FsRedirection(intPtr);
                str = stringBuilder.ToString().Trim();
            }
            catch (Exception exception)
            {
                str = null;
            }
            return str;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        internal static extern void GetNativeSystemInfo(ref RegistryTools.SYSTEM_INFO system_INFO_0);

        public static RegistryTools.Platform GetPlatform()
        {
            RegistryTools.Platform platform;
            bool flag;
            RegistryTools.SYSTEM_INFO sYSTEMINFO = new RegistryTools.SYSTEM_INFO();
            if (Environment.OSVersion.Version.Major > 5)
            {
                flag = false;
            }
            else
            {
                flag = (Environment.OSVersion.Version.Major != 5 ? true : Environment.OSVersion.Version.Minor < 1);
            }
            if (flag)
            {
                RegistryTools.GetSystemInfo(ref sYSTEMINFO);
            }
            else
            {
                RegistryTools.GetNativeSystemInfo(ref sYSTEMINFO);
            }
            ushort num = sYSTEMINFO.wProcessorArchitecture;
            if (num == 0)
            {
                platform = RegistryTools.Platform.X86;
            }
            else
            {
                platform = (num == 6 || num == 9 ? RegistryTools.Platform.X64 : RegistryTools.Platform.Unknown);
            }
            return platform;
        }

        public static string GetRegistryKey(string string_0, string string_1, string string_2)
        {
            string str;
            str = (RegistryTools.GetPlatform() != RegistryTools.Platform.X64 ? RegistryTools.Get32BitRegistryKey(string_0, string_1, string_2) : RegistryTools.Get64BitRegistryKey(string_0, string_1, string_2));
            return str;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        internal static extern void GetSystemInfo(ref RegistryTools.SYSTEM_INFO system_INFO_0);

        private static void old_acctor_mc()
        {
            RegistryTools.HKEY_CLASSES_ROOT = (UIntPtr)(-2147483648);
            RegistryTools.HKEY_CURRENT_USER = (UIntPtr)(-2147483647);
            RegistryTools.HKEY_LOCAL_MACHINE = (UIntPtr)(-2147483646);
            RegistryTools.HKEY_USERS = (UIntPtr)(-2147483645);
            RegistryTools.HKEY_CURRENT_CONFIG = (UIntPtr)(-2147483643);
        }

        [DllImport("Advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern long RegDisableReflectionKey(IntPtr intptr_0);

        [DllImport("Advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern long RegEnableReflectionKey(IntPtr intptr_0);

        [DllImport("Advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern uint RegOpenKeyEx(UIntPtr uintptr_0, string string_0, uint uint_0, int int_0, out IntPtr intptr_0);

        [DllImport("Advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        private static extern int RegQueryValueEx(IntPtr intptr_0, string string_0, int int_0, out uint uint_0, StringBuilder stringBuilder_0, ref uint uint_1);

        private static UIntPtr TransferKeyName(string string_0)
        {
            UIntPtr hKEYCLASSESROOT;
            string string0 = string_0;
            if (string0 == null)
            {
                hKEYCLASSESROOT = RegistryTools.HKEY_CLASSES_ROOT;
                return hKEYCLASSESROOT;
            }
            else if (string0 == "HKEY_CLASSES_ROOT")
            {
                hKEYCLASSESROOT = RegistryTools.HKEY_CLASSES_ROOT;
            }
            else if (string0 == "HKEY_CURRENT_USER")
            {
                hKEYCLASSESROOT = RegistryTools.HKEY_CURRENT_USER;
            }
            else if (string0 == "HKEY_LOCAL_MACHINE")
            {
                hKEYCLASSESROOT = RegistryTools.HKEY_LOCAL_MACHINE;
            }
            else if (string0 == "HKEY_USERS")
            {
                hKEYCLASSESROOT = RegistryTools.HKEY_USERS;
            }
            else
            {
                if (string0 != "HKEY_CURRENT_CONFIG")
                {
                    hKEYCLASSESROOT = RegistryTools.HKEY_CLASSES_ROOT;
                    return hKEYCLASSESROOT;
                }
                hKEYCLASSESROOT = RegistryTools.HKEY_CURRENT_CONFIG;
            }
            return hKEYCLASSESROOT;
        }

        private static RegistryKey TransferKeyName32(string string_0)
        {
            RegistryKey classesRoot;
            string string0 = string_0;
            if (string0 == null)
            {
                classesRoot = Registry.ClassesRoot;
                return classesRoot;
            }
            else if (string0 == "HKEY_CLASSES_ROOT")
            {
                classesRoot = Registry.ClassesRoot;
            }
            else if (string0 == "HKEY_CURRENT_USER")
            {
                classesRoot = Registry.CurrentUser;
            }
            else if (string0 == "HKEY_LOCAL_MACHINE")
            {
                classesRoot = Registry.LocalMachine;
            }
            else if (string0 == "HKEY_USERS")
            {
                classesRoot = Registry.Users;
            }
            else
            {
                if (string0 != "HKEY_CURRENT_CONFIG")
                {
                    classesRoot = Registry.ClassesRoot;
                    return classesRoot;
                }
                classesRoot = Registry.CurrentConfig;
            }
            return classesRoot;
        }

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr intptr_0);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr intptr_0);

        public enum Platform
        {
            X86,
            X64,
            Unknown
        }

        internal struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;

            public ushort wReserved;

            public uint dwPageSize;

            public IntPtr lpMinimumApplicationAddress;

            public IntPtr lpMaximumApplicationAddress;

            public UIntPtr dwActiveProcessorMask;

            public uint dwNumberOfProcessors;

            public uint dwProcessorType;

            public uint dwAllocationGranularity;

            public ushort wProcessorLevel;

            public ushort wProcessorRevision;
        }
    }
}