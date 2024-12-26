using System.Threading.Tasks;

namespace WpfEFCoreStudy.ViewModels.Common;

// https://learn.microsoft.com/ja-jp/archive/msdn-magazine/2014/may/async-programming-patterns-for-asynchronous-mvvm-applications-services

/// <summary>
/// Marks a type as requiring asynchronous initialization and
/// provides the result of that initialization.
/// </summary>
public interface IAsyncInitialization
{

    /// <summary>
    /// The result of the asynchronous initialization of this instance.
    /// </summary>
    Task Initialization { get; }

}
