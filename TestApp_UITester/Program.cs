using repos=TestApp_UITester.repos;

var log = new nac.Logging.Logger();

nac.Logging.Appenders.ColoredConsole.Setup();
        
try
{
    nac.Forms.UITesterApp.TestApp.Run(typeof(repos.TestFunctions.TestFunctions));
            

}catch(Exception ex)
{
    log.Fatal($"App Exception occured: {ex}");
}