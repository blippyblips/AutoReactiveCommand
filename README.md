# AutoReactiveCommand

Fork and re-work of ReactiveCommand.Generator to use the new IncrementalSourceGenerator for ReactiveCommand declaration and initialization by attributing functions with [AutoReactiveCommand].

While it's nice for quickly prototyping stuff or making small apps in this style i wouldn't recommend this for any larger project.

- There's no good way to connect CanExecute when using this and you loose fine grained control over the command creation.

- Secondly, a lot of the functions that you mark end up just relaying to some other class because it's not the ViewModels responsibility.

- Thirdly, when using Reactive, you often want to register other stuff along with your command anyway; such as exception handling and progress tracking which means you're gonna have some code in the constructors anyway.


Some of these things could be solved by adding to this library.
For example could create extra attributes for CanExecute, IsExecuting, ExceptionHandler and Subscription and link them by name parameters in the attributes or by naming convention on the functions.

With that "warning" label on there, it's nice for making minimal utility applications that doesnt involve complex validation etc.