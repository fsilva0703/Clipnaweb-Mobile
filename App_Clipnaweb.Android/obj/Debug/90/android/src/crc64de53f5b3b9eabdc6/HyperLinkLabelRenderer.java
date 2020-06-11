package crc64de53f5b3b9eabdc6;


public class HyperLinkLabelRenderer
	extends crc643f46942d9dd1fff9.LabelRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("XLabs.Forms.Controls.HyperLinkLabelRenderer, XLabs.Forms.Droid", HyperLinkLabelRenderer.class, __md_methods);
	}


	public HyperLinkLabelRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == HyperLinkLabelRenderer.class)
			mono.android.TypeManager.Activate ("XLabs.Forms.Controls.HyperLinkLabelRenderer, XLabs.Forms.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public HyperLinkLabelRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == HyperLinkLabelRenderer.class)
			mono.android.TypeManager.Activate ("XLabs.Forms.Controls.HyperLinkLabelRenderer, XLabs.Forms.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public HyperLinkLabelRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == HyperLinkLabelRenderer.class)
			mono.android.TypeManager.Activate ("XLabs.Forms.Controls.HyperLinkLabelRenderer, XLabs.Forms.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
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
