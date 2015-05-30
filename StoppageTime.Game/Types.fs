module Types 

type GameTime = uint32

type Name = string

type Player = Name

type Goal = {
    Scorer : Player;
    Minute : GameTime;
    Assistance : Player option
}

type Match = {
    GoalsTeam1 : Goal list;
    GoalsTeam2 : Goal list
}

let newMatch () = {
    GoalsTeam1 = [];
    GoalsTeam2 = []
    }

type GameState = {
    Match : Match;
    Time : GameTime
}

(*
let RegularGameTime : GameTime = 90u

type MatchState =
    | PreKickoff
    | Running
    | Paused
    | Done

*)