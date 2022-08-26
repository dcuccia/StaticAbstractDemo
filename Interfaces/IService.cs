// this version of IService causes build error
// decorating concrete classes with IStaticService instead avoids the build error, but does not enforce
// the contract at the IService level
// public interface IService : IStaticService { string ServiceName { get; } }
public interface IService { string ServiceName { get; } }