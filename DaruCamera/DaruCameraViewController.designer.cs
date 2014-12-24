// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace DaruCamera
{
	[Register ("DaruCameraViewController")]
	partial class DaruCameraViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView previewView { get; set; }

		[Action ("UIButton74_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void UIButton74_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (previewView != null) {
				previewView.Dispose ();
				previewView = null;
			}
		}
	}
}
