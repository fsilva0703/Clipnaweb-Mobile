package crc64e005ae2c7b2bb035;


public abstract class BroadcastMonitor
	extends android.content.BroadcastReceiver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("XLabs.Platform.Device.BroadcastMonitor, XLabs.Platform.Droid", BroadcastMonitor.class, __md_methods);
	}


	public BroadcastMonitor ()
	{
		super ();
		if (getClass () == BroadcastMonitor.class)
			mono.android.TypeManager.Activate ("XLabs.Platform.Device.BroadcastMonitor, XLabs.Platform.Droid", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
