using Android.App;
using Android.OS;
using Android.Widget;

namespace EditTextFilter
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var editText = FindViewById<EditText>(Resource.Id.editText);
            editText.SetFilters(new Android.Text.IInputFilter[] { new CapsOnly(true, false, false,true) });

            editText.AfterTextChanged += (s, e) =>
            {
                var text = editText.Text;
                var editable = e.Editable;
            };
        }
    }
}

