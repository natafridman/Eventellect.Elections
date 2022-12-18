# Resolution
For Plurality, first I validate that it is a complete vote, that is, that there are ballots and candidates, in case it is "empty", I throw an exception with the reason. For the counting, I group the ballots by candidate, I also filter if any voter voted for a candidate that is not available, so I get a count of how many voters voted for each candidate, the one with the most votes is the winner. I also handle ties and throw an exception in case it happens. 

In case of Ranked Choice, I perform the same validations and then recursion is used to know who won. The function "Run" receives two parameters, a list of ballots and a list of candidates. The first run filters the ballots by the candidates that are in the list of candidates, if there is a vote for someone who is not available, it is removed. From the count per candidate (of the first choice), the one who has less votes is found and is removed from the list of candidates for the next round, in this way, this candidate is no longer taken into account for the next count, since as I mentioned, the ballots are filtered at the beginning by the list of candidates.

# Requirements
In this solution you will find a base project dealing with Political Elections 

The main goal is to implement the business logic for both election types: Plurality & Ranked Choice. 

For Plurality, the candidate with the most votes wins. There is no minimum vote threshold. 

With Ranked Choice, you will need to consider multiple votes per ballot and their respective ranks.

# Technical
This library was created in Visual Studio 2022 using net6.0 (LTS). If this is an issue, let us know and we can send you what ever project version you need.

# Reference
Please feel free to use any resources necessary to craft your solution. Here are some good election references:

- https://en.wikipedia.org/wiki/Plurality_voting
- https://ballotpedia.org/Ranked-choice_voting_(RCV)
- https://fairvote.org/our-reforms/ranked-choice-voting/

# Refactoring
Feel free to clean up project structure, modify any existing classes, use your favorite patterns, etc.

# Questions?
If you have any questions at all about the requirements, reach out any time and we can go over it.