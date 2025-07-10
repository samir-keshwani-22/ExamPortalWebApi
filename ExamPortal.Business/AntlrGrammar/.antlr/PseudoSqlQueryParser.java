// Generated from d:/ExamPortalWebApi/ExamPortal.Business/AntlrGrammar/PseudoSqlQuery.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue"})
public class PseudoSqlQueryParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		QUERY=1, FROM=2, DATA=3, WHERE=4, AND=5, OR=6, IS=7, PAST=8, BEFORE=9, 
		COUNT=10, SUM=11, IN=12, NOT_IN=13, TRANSACTION_DATE=14, SOURCE_TOKEN=15, 
		DEST_TOKEN=16, AMOUNT=17, EQ=18, NE=19, GT=20, LT=21, GE=22, LE=23, LPAREN=24, 
		RPAREN=25, COMMA=26, STAR=27, TIMEUNIT=28, DATE=29, IDENTIFIER=30, VALUEIDENTIFIER=31, 
		INT=32, WS=33, INVALID=34;
	public static final int
		RULE_start = 0, RULE_query = 1, RULE_aggregateQuery = 2, RULE_aggregateList = 3, 
		RULE_aggregate = 4, RULE_aggregateFunction = 5, RULE_timeFilter = 6, RULE_timeReference = 7, 
		RULE_whereClause = 8, RULE_preExpression = 9, RULE_sourceDestSpecifier = 10, 
		RULE_expression = 11, RULE_term = 12, RULE_field = 13, RULE_operator = 14, 
		RULE_value = 15, RULE_logicalOperator = 16;
	private static String[] makeRuleNames() {
		return new String[] {
			"start", "query", "aggregateQuery", "aggregateList", "aggregate", "aggregateFunction", 
			"timeFilter", "timeReference", "whereClause", "preExpression", "sourceDestSpecifier", 
			"expression", "term", "field", "operator", "value", "logicalOperator"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'QUERY'", "'FROM'", "'DATA'", "'WHERE'", "'AND'", "'OR'", "'is'", 
			"'PAST'", "'before'", "'COUNT'", "'SUM'", "'in'", null, "'transaction date'", 
			"'{source}'", "'{dest}'", "'{amount}'", "'='", "'!='", "'>'", "'<'", 
			"'>='", "'<='", "'('", "')'", "','", "'*'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "QUERY", "FROM", "DATA", "WHERE", "AND", "OR", "IS", "PAST", "BEFORE", 
			"COUNT", "SUM", "IN", "NOT_IN", "TRANSACTION_DATE", "SOURCE_TOKEN", "DEST_TOKEN", 
			"AMOUNT", "EQ", "NE", "GT", "LT", "GE", "LE", "LPAREN", "RPAREN", "COMMA", 
			"STAR", "TIMEUNIT", "DATE", "IDENTIFIER", "VALUEIDENTIFIER", "INT", "WS", 
			"INVALID"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "PseudoSqlQuery.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public PseudoSqlQueryParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@SuppressWarnings("CheckReturnValue")
	public static class StartContext extends ParserRuleContext {
		public QueryContext query() {
			return getRuleContext(QueryContext.class,0);
		}
		public TerminalNode EOF() { return getToken(PseudoSqlQueryParser.EOF, 0); }
		public StartContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_start; }
	}

	public final StartContext start() throws RecognitionException {
		StartContext _localctx = new StartContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_start);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(34);
			query();
			setState(35);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class QueryContext extends ParserRuleContext {
		public AggregateQueryContext aggregateQuery() {
			return getRuleContext(AggregateQueryContext.class,0);
		}
		public QueryContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_query; }
	}

	public final QueryContext query() throws RecognitionException {
		QueryContext _localctx = new QueryContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_query);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(37);
			aggregateQuery();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class AggregateQueryContext extends ParserRuleContext {
		public TerminalNode QUERY() { return getToken(PseudoSqlQueryParser.QUERY, 0); }
		public AggregateListContext aggregateList() {
			return getRuleContext(AggregateListContext.class,0);
		}
		public TerminalNode FROM() { return getToken(PseudoSqlQueryParser.FROM, 0); }
		public TerminalNode DATA() { return getToken(PseudoSqlQueryParser.DATA, 0); }
		public TimeFilterContext timeFilter() {
			return getRuleContext(TimeFilterContext.class,0);
		}
		public WhereClauseContext whereClause() {
			return getRuleContext(WhereClauseContext.class,0);
		}
		public AggregateQueryContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggregateQuery; }
	}

	public final AggregateQueryContext aggregateQuery() throws RecognitionException {
		AggregateQueryContext _localctx = new AggregateQueryContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_aggregateQuery);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(39);
			match(QUERY);
			setState(40);
			aggregateList();
			setState(41);
			match(FROM);
			setState(42);
			match(DATA);
			setState(44);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==PAST) {
				{
				setState(43);
				timeFilter();
				}
			}

			setState(47);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==WHERE) {
				{
				setState(46);
				whereClause();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class AggregateListContext extends ParserRuleContext {
		public List<AggregateContext> aggregate() {
			return getRuleContexts(AggregateContext.class);
		}
		public AggregateContext aggregate(int i) {
			return getRuleContext(AggregateContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(PseudoSqlQueryParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(PseudoSqlQueryParser.COMMA, i);
		}
		public AggregateListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggregateList; }
	}

	public final AggregateListContext aggregateList() throws RecognitionException {
		AggregateListContext _localctx = new AggregateListContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_aggregateList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(49);
			aggregate();
			setState(54);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(50);
				match(COMMA);
				setState(51);
				aggregate();
				}
				}
				setState(56);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class AggregateContext extends ParserRuleContext {
		public AggregateFunctionContext aggregateFunction() {
			return getRuleContext(AggregateFunctionContext.class,0);
		}
		public TerminalNode LPAREN() { return getToken(PseudoSqlQueryParser.LPAREN, 0); }
		public TerminalNode RPAREN() { return getToken(PseudoSqlQueryParser.RPAREN, 0); }
		public TerminalNode AMOUNT() { return getToken(PseudoSqlQueryParser.AMOUNT, 0); }
		public AggregateContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggregate; }
	}

	public final AggregateContext aggregate() throws RecognitionException {
		AggregateContext _localctx = new AggregateContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_aggregate);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(57);
			aggregateFunction();
			setState(58);
			match(LPAREN);
			{
			setState(59);
			match(AMOUNT);
			}
			setState(60);
			match(RPAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class AggregateFunctionContext extends ParserRuleContext {
		public TerminalNode COUNT() { return getToken(PseudoSqlQueryParser.COUNT, 0); }
		public TerminalNode SUM() { return getToken(PseudoSqlQueryParser.SUM, 0); }
		public AggregateFunctionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggregateFunction; }
	}

	public final AggregateFunctionContext aggregateFunction() throws RecognitionException {
		AggregateFunctionContext _localctx = new AggregateFunctionContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_aggregateFunction);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(62);
			_la = _input.LA(1);
			if ( !(_la==COUNT || _la==SUM) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class TimeFilterContext extends ParserRuleContext {
		public TerminalNode PAST() { return getToken(PseudoSqlQueryParser.PAST, 0); }
		public TerminalNode INT() { return getToken(PseudoSqlQueryParser.INT, 0); }
		public TerminalNode TIMEUNIT() { return getToken(PseudoSqlQueryParser.TIMEUNIT, 0); }
		public TerminalNode FROM() { return getToken(PseudoSqlQueryParser.FROM, 0); }
		public TimeReferenceContext timeReference() {
			return getRuleContext(TimeReferenceContext.class,0);
		}
		public TimeFilterContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_timeFilter; }
	}

	public final TimeFilterContext timeFilter() throws RecognitionException {
		TimeFilterContext _localctx = new TimeFilterContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_timeFilter);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(64);
			match(PAST);
			setState(65);
			match(INT);
			setState(66);
			match(TIMEUNIT);
			setState(67);
			match(FROM);
			setState(68);
			timeReference();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class TimeReferenceContext extends ParserRuleContext {
		public TerminalNode TRANSACTION_DATE() { return getToken(PseudoSqlQueryParser.TRANSACTION_DATE, 0); }
		public TerminalNode INT() { return getToken(PseudoSqlQueryParser.INT, 0); }
		public TerminalNode TIMEUNIT() { return getToken(PseudoSqlQueryParser.TIMEUNIT, 0); }
		public TerminalNode BEFORE() { return getToken(PseudoSqlQueryParser.BEFORE, 0); }
		public TimeReferenceContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_timeReference; }
	}

	public final TimeReferenceContext timeReference() throws RecognitionException {
		TimeReferenceContext _localctx = new TimeReferenceContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_timeReference);
		try {
			setState(75);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case TRANSACTION_DATE:
				enterOuterAlt(_localctx, 1);
				{
				setState(70);
				match(TRANSACTION_DATE);
				}
				break;
			case INT:
				enterOuterAlt(_localctx, 2);
				{
				setState(71);
				match(INT);
				setState(72);
				match(TIMEUNIT);
				setState(73);
				match(BEFORE);
				setState(74);
				match(TRANSACTION_DATE);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class WhereClauseContext extends ParserRuleContext {
		public TerminalNode WHERE() { return getToken(PseudoSqlQueryParser.WHERE, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public PreExpressionContext preExpression() {
			return getRuleContext(PreExpressionContext.class,0);
		}
		public WhereClauseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_whereClause; }
	}

	public final WhereClauseContext whereClause() throws RecognitionException {
		WhereClauseContext _localctx = new WhereClauseContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_whereClause);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(77);
			match(WHERE);
			setState(79);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,4,_ctx) ) {
			case 1:
				{
				setState(78);
				preExpression();
				}
				break;
			}
			setState(81);
			expression();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class PreExpressionContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(PseudoSqlQueryParser.IDENTIFIER, 0); }
		public TerminalNode IS() { return getToken(PseudoSqlQueryParser.IS, 0); }
		public SourceDestSpecifierContext sourceDestSpecifier() {
			return getRuleContext(SourceDestSpecifierContext.class,0);
		}
		public TerminalNode AND() { return getToken(PseudoSqlQueryParser.AND, 0); }
		public PreExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_preExpression; }
	}

	public final PreExpressionContext preExpression() throws RecognitionException {
		PreExpressionContext _localctx = new PreExpressionContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_preExpression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(83);
			match(IDENTIFIER);
			setState(84);
			match(IS);
			setState(85);
			sourceDestSpecifier();
			setState(86);
			match(AND);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class SourceDestSpecifierContext extends ParserRuleContext {
		public TerminalNode SOURCE_TOKEN() { return getToken(PseudoSqlQueryParser.SOURCE_TOKEN, 0); }
		public TerminalNode DEST_TOKEN() { return getToken(PseudoSqlQueryParser.DEST_TOKEN, 0); }
		public SourceDestSpecifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_sourceDestSpecifier; }
	}

	public final SourceDestSpecifierContext sourceDestSpecifier() throws RecognitionException {
		SourceDestSpecifierContext _localctx = new SourceDestSpecifierContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_sourceDestSpecifier);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(88);
			_la = _input.LA(1);
			if ( !(_la==SOURCE_TOKEN || _la==DEST_TOKEN) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ExpressionContext extends ParserRuleContext {
		public List<TermContext> term() {
			return getRuleContexts(TermContext.class);
		}
		public TermContext term(int i) {
			return getRuleContext(TermContext.class,i);
		}
		public List<LogicalOperatorContext> logicalOperator() {
			return getRuleContexts(LogicalOperatorContext.class);
		}
		public LogicalOperatorContext logicalOperator(int i) {
			return getRuleContext(LogicalOperatorContext.class,i);
		}
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	}

	public final ExpressionContext expression() throws RecognitionException {
		ExpressionContext _localctx = new ExpressionContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(90);
			term();
			setState(96);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==AND || _la==OR) {
				{
				{
				setState(91);
				logicalOperator();
				setState(92);
				term();
				}
				}
				setState(98);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class TermContext extends ParserRuleContext {
		public TerminalNode LPAREN() { return getToken(PseudoSqlQueryParser.LPAREN, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode RPAREN() { return getToken(PseudoSqlQueryParser.RPAREN, 0); }
		public FieldContext field() {
			return getRuleContext(FieldContext.class,0);
		}
		public OperatorContext operator() {
			return getRuleContext(OperatorContext.class,0);
		}
		public ValueContext value() {
			return getRuleContext(ValueContext.class,0);
		}
		public TermContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_term; }
	}

	public final TermContext term() throws RecognitionException {
		TermContext _localctx = new TermContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_term);
		try {
			setState(107);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LPAREN:
				enterOuterAlt(_localctx, 1);
				{
				setState(99);
				match(LPAREN);
				setState(100);
				expression();
				setState(101);
				match(RPAREN);
				}
				break;
			case IDENTIFIER:
				enterOuterAlt(_localctx, 2);
				{
				setState(103);
				field();
				setState(104);
				operator();
				setState(105);
				value();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class FieldContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(PseudoSqlQueryParser.IDENTIFIER, 0); }
		public FieldContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_field; }
	}

	public final FieldContext field() throws RecognitionException {
		FieldContext _localctx = new FieldContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_field);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(109);
			match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class OperatorContext extends ParserRuleContext {
		public TerminalNode EQ() { return getToken(PseudoSqlQueryParser.EQ, 0); }
		public TerminalNode NE() { return getToken(PseudoSqlQueryParser.NE, 0); }
		public TerminalNode GT() { return getToken(PseudoSqlQueryParser.GT, 0); }
		public TerminalNode LT() { return getToken(PseudoSqlQueryParser.LT, 0); }
		public TerminalNode GE() { return getToken(PseudoSqlQueryParser.GE, 0); }
		public TerminalNode LE() { return getToken(PseudoSqlQueryParser.LE, 0); }
		public TerminalNode IN() { return getToken(PseudoSqlQueryParser.IN, 0); }
		public TerminalNode NOT_IN() { return getToken(PseudoSqlQueryParser.NOT_IN, 0); }
		public OperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_operator; }
	}

	public final OperatorContext operator() throws RecognitionException {
		OperatorContext _localctx = new OperatorContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(111);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 16527360L) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ValueContext extends ParserRuleContext {
		public TerminalNode VALUEIDENTIFIER() { return getToken(PseudoSqlQueryParser.VALUEIDENTIFIER, 0); }
		public TerminalNode DATE() { return getToken(PseudoSqlQueryParser.DATE, 0); }
		public ValueContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_value; }
	}

	public final ValueContext value() throws RecognitionException {
		ValueContext _localctx = new ValueContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_value);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(113);
			_la = _input.LA(1);
			if ( !(_la==DATE || _la==VALUEIDENTIFIER) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class LogicalOperatorContext extends ParserRuleContext {
		public TerminalNode AND() { return getToken(PseudoSqlQueryParser.AND, 0); }
		public TerminalNode OR() { return getToken(PseudoSqlQueryParser.OR, 0); }
		public LogicalOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_logicalOperator; }
	}

	public final LogicalOperatorContext logicalOperator() throws RecognitionException {
		LogicalOperatorContext _localctx = new LogicalOperatorContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_logicalOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(115);
			_la = _input.LA(1);
			if ( !(_la==AND || _la==OR) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static final String _serializedATN =
		"\u0004\u0001\"v\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001\u0002"+
		"\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004\u0007\u0004\u0002"+
		"\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007\u0007\u0007\u0002"+
		"\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002\u000b\u0007\u000b\u0002"+
		"\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e\u0002\u000f\u0007\u000f"+
		"\u0002\u0010\u0007\u0010\u0001\u0000\u0001\u0000\u0001\u0000\u0001\u0001"+
		"\u0001\u0001\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002"+
		"\u0003\u0002-\b\u0002\u0001\u0002\u0003\u00020\b\u0002\u0001\u0003\u0001"+
		"\u0003\u0001\u0003\u0005\u00035\b\u0003\n\u0003\f\u00038\t\u0003\u0001"+
		"\u0004\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0005\u0001"+
		"\u0005\u0001\u0006\u0001\u0006\u0001\u0006\u0001\u0006\u0001\u0006\u0001"+
		"\u0006\u0001\u0007\u0001\u0007\u0001\u0007\u0001\u0007\u0001\u0007\u0003"+
		"\u0007L\b\u0007\u0001\b\u0001\b\u0003\bP\b\b\u0001\b\u0001\b\u0001\t\u0001"+
		"\t\u0001\t\u0001\t\u0001\t\u0001\n\u0001\n\u0001\u000b\u0001\u000b\u0001"+
		"\u000b\u0001\u000b\u0005\u000b_\b\u000b\n\u000b\f\u000bb\t\u000b\u0001"+
		"\f\u0001\f\u0001\f\u0001\f\u0001\f\u0001\f\u0001\f\u0001\f\u0003\fl\b"+
		"\f\u0001\r\u0001\r\u0001\u000e\u0001\u000e\u0001\u000f\u0001\u000f\u0001"+
		"\u0010\u0001\u0010\u0001\u0010\u0000\u0000\u0011\u0000\u0002\u0004\u0006"+
		"\b\n\f\u000e\u0010\u0012\u0014\u0016\u0018\u001a\u001c\u001e \u0000\u0005"+
		"\u0001\u0000\n\u000b\u0001\u0000\u000f\u0010\u0002\u0000\f\r\u0012\u0017"+
		"\u0002\u0000\u001d\u001d\u001f\u001f\u0001\u0000\u0005\u0006k\u0000\""+
		"\u0001\u0000\u0000\u0000\u0002%\u0001\u0000\u0000\u0000\u0004\'\u0001"+
		"\u0000\u0000\u0000\u00061\u0001\u0000\u0000\u0000\b9\u0001\u0000\u0000"+
		"\u0000\n>\u0001\u0000\u0000\u0000\f@\u0001\u0000\u0000\u0000\u000eK\u0001"+
		"\u0000\u0000\u0000\u0010M\u0001\u0000\u0000\u0000\u0012S\u0001\u0000\u0000"+
		"\u0000\u0014X\u0001\u0000\u0000\u0000\u0016Z\u0001\u0000\u0000\u0000\u0018"+
		"k\u0001\u0000\u0000\u0000\u001am\u0001\u0000\u0000\u0000\u001co\u0001"+
		"\u0000\u0000\u0000\u001eq\u0001\u0000\u0000\u0000 s\u0001\u0000\u0000"+
		"\u0000\"#\u0003\u0002\u0001\u0000#$\u0005\u0000\u0000\u0001$\u0001\u0001"+
		"\u0000\u0000\u0000%&\u0003\u0004\u0002\u0000&\u0003\u0001\u0000\u0000"+
		"\u0000\'(\u0005\u0001\u0000\u0000()\u0003\u0006\u0003\u0000)*\u0005\u0002"+
		"\u0000\u0000*,\u0005\u0003\u0000\u0000+-\u0003\f\u0006\u0000,+\u0001\u0000"+
		"\u0000\u0000,-\u0001\u0000\u0000\u0000-/\u0001\u0000\u0000\u0000.0\u0003"+
		"\u0010\b\u0000/.\u0001\u0000\u0000\u0000/0\u0001\u0000\u0000\u00000\u0005"+
		"\u0001\u0000\u0000\u000016\u0003\b\u0004\u000023\u0005\u001a\u0000\u0000"+
		"35\u0003\b\u0004\u000042\u0001\u0000\u0000\u000058\u0001\u0000\u0000\u0000"+
		"64\u0001\u0000\u0000\u000067\u0001\u0000\u0000\u00007\u0007\u0001\u0000"+
		"\u0000\u000086\u0001\u0000\u0000\u00009:\u0003\n\u0005\u0000:;\u0005\u0018"+
		"\u0000\u0000;<\u0005\u0011\u0000\u0000<=\u0005\u0019\u0000\u0000=\t\u0001"+
		"\u0000\u0000\u0000>?\u0007\u0000\u0000\u0000?\u000b\u0001\u0000\u0000"+
		"\u0000@A\u0005\b\u0000\u0000AB\u0005 \u0000\u0000BC\u0005\u001c\u0000"+
		"\u0000CD\u0005\u0002\u0000\u0000DE\u0003\u000e\u0007\u0000E\r\u0001\u0000"+
		"\u0000\u0000FL\u0005\u000e\u0000\u0000GH\u0005 \u0000\u0000HI\u0005\u001c"+
		"\u0000\u0000IJ\u0005\t\u0000\u0000JL\u0005\u000e\u0000\u0000KF\u0001\u0000"+
		"\u0000\u0000KG\u0001\u0000\u0000\u0000L\u000f\u0001\u0000\u0000\u0000"+
		"MO\u0005\u0004\u0000\u0000NP\u0003\u0012\t\u0000ON\u0001\u0000\u0000\u0000"+
		"OP\u0001\u0000\u0000\u0000PQ\u0001\u0000\u0000\u0000QR\u0003\u0016\u000b"+
		"\u0000R\u0011\u0001\u0000\u0000\u0000ST\u0005\u001e\u0000\u0000TU\u0005"+
		"\u0007\u0000\u0000UV\u0003\u0014\n\u0000VW\u0005\u0005\u0000\u0000W\u0013"+
		"\u0001\u0000\u0000\u0000XY\u0007\u0001\u0000\u0000Y\u0015\u0001\u0000"+
		"\u0000\u0000Z`\u0003\u0018\f\u0000[\\\u0003 \u0010\u0000\\]\u0003\u0018"+
		"\f\u0000]_\u0001\u0000\u0000\u0000^[\u0001\u0000\u0000\u0000_b\u0001\u0000"+
		"\u0000\u0000`^\u0001\u0000\u0000\u0000`a\u0001\u0000\u0000\u0000a\u0017"+
		"\u0001\u0000\u0000\u0000b`\u0001\u0000\u0000\u0000cd\u0005\u0018\u0000"+
		"\u0000de\u0003\u0016\u000b\u0000ef\u0005\u0019\u0000\u0000fl\u0001\u0000"+
		"\u0000\u0000gh\u0003\u001a\r\u0000hi\u0003\u001c\u000e\u0000ij\u0003\u001e"+
		"\u000f\u0000jl\u0001\u0000\u0000\u0000kc\u0001\u0000\u0000\u0000kg\u0001"+
		"\u0000\u0000\u0000l\u0019\u0001\u0000\u0000\u0000mn\u0005\u001e\u0000"+
		"\u0000n\u001b\u0001\u0000\u0000\u0000op\u0007\u0002\u0000\u0000p\u001d"+
		"\u0001\u0000\u0000\u0000qr\u0007\u0003\u0000\u0000r\u001f\u0001\u0000"+
		"\u0000\u0000st\u0007\u0004\u0000\u0000t!\u0001\u0000\u0000\u0000\u0007"+
		",/6KO`k";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}