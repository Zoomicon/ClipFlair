//Version: 20120704

/*
NOTE: Please add 
  
#if SILVERLIGHT
using Decorator = System.Windows.Controls.Border;
#endif  

at your source code file and then use Dectorator (instead of Border) in both WPF and Silverlight in your source code
 (WPF's Border extends WPF Decorator, whereas Silverlight's Border doesn't since Silerlight doesn't have Decorator class)
  
*/