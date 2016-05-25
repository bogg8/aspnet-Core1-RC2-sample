namespace OdeToFood.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    using OdeToFood.Services;

    public class Greeting : ViewComponent
    {
        private IGreeter _greeter;

        public Greeting(IGreeter greeter)
        {
            _greeter = greeter;
        }

        public IViewComponentResult Invoke()
        {
            var model = this._greeter.GetGreeting();
            return this.View(model: model);
        }
    }
}
