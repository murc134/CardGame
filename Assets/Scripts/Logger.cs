using UnityEngine;
using System.IO;
using UnityEditor;

public static class Logger
{
    private static string logPrefix = "[CB] ";
    private static string logFilePath = "log.txt";

    public static void Log(object message, GameObject obj = null)
    {
        Debug.Log(logPrefix + message, obj);
        WriteString(message.ToString());
    }

    public static void LogWarning(object message, GameObject obj = null)
    {
        Debug.LogWarning(logPrefix + message, obj);
        WriteString("Warning: " + message.ToString());
    }

    public static void LogError(object message, GameObject obj = null)
    {
        Debug.LogError(logPrefix + message, obj);
        WriteString("Error: " + message.ToString());
    }

    public static void Log(Object context, object message)
    {
        Debug.Log(logPrefix + message, context);
        WriteString(message.ToString());
    }

    public static void LogWarning(Object context, object message)
    {
        Debug.LogWarning(logPrefix + message, context);
        WriteString("Warning: " + message.ToString());
    }

    public static void LogError(Object context, object message)
    {
        Debug.LogError(logPrefix + message, context);
        WriteString("Error: " + message.ToString());
    }

    private static void WriteString(string line)
    {
        string path = Application.persistentDataPath + "/test.txt";

        // Read the existing contents of the file (if any)
        string existingContent = "";
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                existingContent = reader.ReadToEnd();
            }
        }

        // Append or overwrite the file with new content
        using (StreamWriter writer = new StreamWriter(path, false)) // Use 'false' to overwrite the file
        {
            // Write the new content
            writer.WriteLine(line);

            // Append the existing content
            writer.Write(existingContent);
        }

    }
}
