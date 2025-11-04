using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPSD.WinFormsApp.Model;

namespace SPSD.WinFormsApp;

public static class LocalEventBus
{
    public static event Action<RequestShowForm>? EventShowForm;

    public static void SubjectEventAsync(Action<RequestShowForm> func)
    {
        EventShowForm = func;
    }

    public static void PublishShowFormEventAsync(RequestShowForm request)
    {
        if (EventShowForm != null)
        {
            EventShowForm.Invoke(request);
        }
    }

    public static bool IsPhotographed { get; set; }

    public static event Func<CreateTgFileModel, Task<string>>? EventCreateTgFile;


    public static async Task<string> PublishCreateTgFileEventAsync(CreateTgFileModel request)
    {
        if (EventCreateTgFile != null)
        {
           return await EventCreateTgFile.Invoke(request);
        }
        return string.Empty;
    }


}