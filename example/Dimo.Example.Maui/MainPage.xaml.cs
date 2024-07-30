using System.Diagnostics;
using Dimo.Client.Core.Services.DeviceData;

namespace Dimo.Example.Maui;

public partial class MainPage : ContentPage
{
    private readonly IDeviceDataService _deviceDataService;

    public MainPage(IDeviceDataService deviceDataService)
    {
        _deviceDataService = deviceDataService;
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        
        try
        {
            var response = await _deviceDataService.GetVehicleHistoryAsync(1234567890);

            Debug.WriteLine(response.Shards.Successful);
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);
        }
        
    }
}