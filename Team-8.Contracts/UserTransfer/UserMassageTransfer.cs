using Team_8.Contracts.Enums;

namespace Team_8.Contracts.UserTransfer;

public record UserMassageTransfer(Microservice Microservice, PurposeOfTheMessage Purpose, string TypeMessage, string Message);