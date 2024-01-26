@echo off
@echo Creating Global Topics

REM SET /P zookeeper=Enter the number of replication: 
If [%replication%] == [] set replication=1

REM SET /P zookeeper=Enter the number of partitions: 
If [%partitions_number%] == [] set partitions_number=4

SET /P bootstrap_servers=Enter the bootstrap servers - comma-separated list of IP:Port pairs: 
If [%bootstrap_servers%] == [] (
    @echo bootstrap servers - comma-separated list of IP:Port pairs - must be provided!
    exit /b 1
)

set dockerImage=confluentinc/cp-enterprise-kafka:5.4.2

set topics=GameStart GameComplete GameSideBet GamesBalancesDetail
(for %%t in (%topics%) do (
	@echo 'Creating %%t topic'
	docker run --rm %dockerImage% kafka-topics --create --topic %%t --bootstrap-server %bootstrap_servers% --config cleanup.policy=$policy --replication-factor %replication% --partitions %partitions_number%
))

set topics=BJGameStart BJGameComplete BJPlayerHands
(for %%t in (%topics%) do (
	@echo 'Creating %%t topic'
	docker run --rm %dockerImage% kafka-topics --create --topic %%t --bootstrap-server %bootstrap_servers% --config cleanup.policy=$policy --replication-factor %replication% --partitions %partitions_number%
))

set topics=BingoGameDataStarted BingoGameDataCompleted
(for %%t in (%topics%) do (
	@echo 'Creating %%t topic'
	docker run --rm %dockerImage% kafka-topics --create --topic %%t --bootstrap-server %bootstrap_servers% --config cleanup.policy=$policy --replication-factor %replication% --partitions %partitions_number%
))

set topics=KenoGameStart KenoGameComplete
(for %%t in (%topics%) do (
	@echo 'Creating %%t topic'
	docker run --rm %dockerImage% kafka-topics --create --topic %%t --bootstrap-server %bootstrap_servers% --config cleanup.policy=$policy --replication-factor %replication% --partitions %partitions_number%
))

set topics=MHVPokerGameStart MHVPokerGameComplete
(for %%t in (%topics%) do (
	@echo 'Creating %%t topic'
	docker run --rm %dockerImage% kafka-topics --create --topic %%t --bootstrap-server %bootstrap_servers% --config cleanup.policy=$policy --replication-factor %replication% --partitions %partitions_number%
))

set topics=SlotsGameStart SlotsGameComplete
(for %%t in (%topics%) do (
	@echo 'Creating %%t topic'
	docker run --rm %dockerImage% kafka-topics --create --topic %%t --bootstrap-server %bootstrap_servers% --config cleanup.policy=$policy --replication-factor %replication% --partitions %partitions_number%
))

set topics=BaccaratGames BonusBingoGames EPokerGames CDrawPokerGames CHoldemPokerGames CrapsGames CStudPokerGames LetEmRideGames PGPokerGames RedDogGames RouletteGames SevenCardStudGames SicBoGames ScratchCardGames THoldemBonusPokerGames ThreeCardRummyGames ThreeCardStudGames WarGames
(for %%t in (%topics%) do (
	@echo 'Creating %%t topic'
	docker run --rm %dockerImage% kafka-topics --create --topic %%t --bootstrap-server %bootstrap_servers% --config cleanup.policy=$policy --replication-factor %replication% --partitions %partitions_number%
))

set topics=VPokerGameStart VPokerGameComplete
(for %%t in (%topics%) do (
	@echo 'Creating %%t topic'
	docker run --rm %dockerImage% kafka-topics --create --topic %%t --bootstrap-server %bootstrap_servers% --config cleanup.policy=$policy --replication-factor %replication% --partitions %partitions_number%
))

set topics=TournamentPlayerAlias TournamentPlayerAlias-retry TournamentData TournamentData-retry
(for %%t in (%topics%) do (
	@echo 'Creating %%t topic'
	docker run --rm %dockerImage% kafka-topics --create --topic %%t --bootstrap-server %bootstrap_servers% --config cleanup.policy=$policy --replication-factor %replication% --partitions %partitions_number%
))

