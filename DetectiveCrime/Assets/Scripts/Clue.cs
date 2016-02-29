public struct Clue {
    public string Statement;
    public bool IsLying;
    public string SecondStatement;
    public bool IsMurderer {
        get { return SecondStatement == "I don't want to talk to you!" && IsLying; }
    }

    public Clue(string statement) : this(statement, false, "I don't want to talk to you!") { }
    public Clue(string statement, bool isLying) : this(statement, isLying, "I don't want to talk to you!") { }
    public Clue(string statement, string secondStatement) : this(statement, true, secondStatement) { }
    public Clue(string statement, bool isLying, string secondStatement) {
        Statement = statement;
        IsLying = isLying;
        SecondStatement = secondStatement;
    }
}
