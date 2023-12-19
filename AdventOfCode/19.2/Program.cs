using _19._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);

var workFlows = new List<WorkFlow>();

int line = 0;
while (!string.IsNullOrEmpty(inputLines[line]))
{
    workFlows.Add(new WorkFlow(inputLines[line], line));
    line++;
}

WorkFlow startingWorkFlow = workFlows.First(w => w.Name == "in");
var approvingRules = workFlows.Select(w => w.Rules).SelectMany(r => r).Where(r => r.WorkFlowName == "A");
long totalPossibilities = 0;
foreach (Rule rule in approvingRules)
{
    Rule currentRule = rule;
    int ruleIndex = rule.RuleIndex;
    WorkFlow workFlow = workFlows[rule.WorkFlowIndex];
    var rulePath = new List<(Rule rule, bool passed)> { (rule, true) };
    while (workFlow != startingWorkFlow || ruleIndex != 0)
    {
        if (ruleIndex == 0)
        { 
            string previousWorkFlowName = workFlow.Name;
            workFlow = workFlows.Where(w => w.Rules.Any(r => r.WorkFlowName == workFlow.Name)).Single();
            currentRule = workFlow.Rules.Single(r => r.WorkFlowName == previousWorkFlowName);
            ruleIndex = currentRule.RuleIndex;
            rulePath.Add((currentRule, true));
        }
        else
        {
            ruleIndex--;
            currentRule = workFlow.Rules[ruleIndex];
            rulePath.Add((currentRule, false));
        }
    }

    int minX, minM, minA, minS, maxX, maxM, maxA, maxS;
    minX = minM = minA = minS = 1;
    maxX = maxM = maxA = maxS = 4000;

    foreach (var rip in rulePath)
        EvaluateRule(rip.rule, rip.passed, ref minX, ref minM, ref minA, ref minS, ref maxX, ref maxM, ref maxA, ref maxS);

    long xPossibilities = Math.Max(0, maxX - minX + 1);
    long mPossibilities = Math.Max(0, maxM - minM + 1);
    long aPossibilities = Math.Max(0, maxA - minA + 1);
    long sPossibilities = Math.Max(0, maxS - minS + 1);
    totalPossibilities += xPossibilities * mPossibilities * aPossibilities * sPossibilities;
}

Console.WriteLine(totalPossibilities);


void EvaluateRule(Rule rule, bool passed, ref int minX, ref int minM, ref int minA, ref int minS, ref int maxX, ref int maxM, ref int maxA, ref int maxS)
{
    Operator @operator = rule.Operator switch
    {
        Operator.MoreThan => passed ? Operator.MoreThan : Operator.LessThan,
        Operator.LessThan => passed ? Operator.LessThan : Operator.MoreThan,
        Operator.None => Operator.None,
        _ => throw new NotImplementedException()
    };

    int mod = passed ? -1 : 0;
    minX = rule.Category switch
    {
        Category.ExtremelyCoolLooking => @operator == Operator.MoreThan ? Math.Max(rule.ComparedValue - mod, minX) : minX,
        _ => minX
    };

    minM = rule.Category switch
    {
        Category.Musical => @operator == Operator.MoreThan ? Math.Max(rule.ComparedValue - mod, minM) : minM,
        _ => minM
    };

    minA = rule.Category switch
    {
        Category.Aerodynamic => @operator == Operator.MoreThan ? Math.Max(rule.ComparedValue - mod, minA) : minA,
        _ => minA
    };

    minS = rule.Category switch
    {
        Category.Shiny => @operator == Operator.MoreThan ? Math.Max(rule.ComparedValue - mod, minS) : minS,
        _ => minS
    };

    maxX = rule.Category switch
    {
        Category.ExtremelyCoolLooking => @operator == Operator.LessThan ? Math.Min(rule.ComparedValue + mod, maxX) : maxX,
        _ => maxX
    };

    maxM = rule.Category switch
    {
        Category.Musical => @operator == Operator.LessThan ? Math.Min(rule.ComparedValue + mod, maxM) : maxM,
        _ => maxM
    };

    maxA = rule.Category switch
    {
        Category.Aerodynamic => @operator == Operator.LessThan ? Math.Min(rule.ComparedValue + mod, maxA) : maxA,
        _ => maxA
    };

    maxS = rule.Category switch
    {
        Category.Shiny => @operator == Operator.LessThan ? Math.Min(rule.ComparedValue + mod, maxS) : maxS,
        _ => maxS
    };
}

