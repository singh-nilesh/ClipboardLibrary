using System.Net;

namespace clipboardLibrary.Views;

public partial class PdfPage : ContentPage
{
	public PdfPage()
	{
		InitializeComponent();

#if ANDROID
		Microsoft.Maui.Handlers.WebViewHandler.Mapper.AppendToMapping("PdfReader",
			(handler, View) =>
			{
				handler.PlatformView.Settings.AllowFileAccess = true;
				handler.PlatformView.Settings.AllowFileAccessFromFileURLs = true;
				handler.PlatformView.Settings.AllowUniversalAccessFromFileURLs = true;
			});

		pdfView.Source = $"file:///android_asset/pdfjs/web/viewer.html?file=file:///android_asset/{WebUtility.UrlEncode("Manual.pdf")}";

#else
		pdfView.Source = "Manual.pdf";
#endif
    }
}