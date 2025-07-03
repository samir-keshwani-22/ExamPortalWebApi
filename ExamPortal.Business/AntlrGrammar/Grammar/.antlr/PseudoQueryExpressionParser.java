// Generated from d:/ExamPortalWebApi/ExamPortal.Business/AntlrGrammar/Grammar/PseudoQueryExpression.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue"})
public class PseudoQueryExpressionParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		QUERY=1, FROM=2, DATA=3, WHERE=4, AND=5, OR=6, IS=7, PAST=8, BEFORE=9, 
		COUNT=10, SUM=11, IN=12, NOT_IN=13, TRANSACTION_DATE=14, SOURCE_TOKEN=15, 
		DEST_TOKEN=16, EQ=17, NE=18, GT=19, LT=20, GE=21, LE=22, LPAREN=23, RPAREN=24, 
		COMMA=25, STAR=26, TIMEUNIT=27, DATE=28, IDENTIFIER=29, VALUEIDENTIFIER=30, 
		EMPTY=31, INT=32, WS=33, INVALID=34;
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
			"'{source}'", "'{dest}'", "'='", "'!='", "'>'", "'<'", "'>='", "'<='", 
			"'('", "')'", "','", "'*'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "QUERY", "FROM", "DATA", "WHERE", "AND", "OR", "IS", "PAST", "BEFORE", 
			"COUNT", "SUM", "IN", "NOT_IN", "TRANSACTION_DATE", "SOURCE_TOKEN", "DEST_TOKEN", 
			"EQ", "NE", "GT", "LT", "GE", "LE", "LPAREN", "RPAREN", "COMMA", "STAR", 
			"TIMEUNIT", "DATE", "IDENTIFIER", "VALUEIDENTIFIER", "EMPTY", "INT", 
			"WS", "INVALID"
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
	public String getGrammarFileName() { return "PseudoQueryExpression.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public PseudoQueryExpressionParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@SuppressWarnings("CheckReturnValue")
	public static class StartContext extends ParserRuleContext {
		public QueryContext query() {
			return getRuleContext(QueryContext.class,0);
		}
		public TerminalNode EOF() { return getToken(PseudoQueryExpressionParser.EOF, 0); }
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
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public PreExpressionContext preExpression() {
			return getRuleContext(PreExpressionContext.class,0);
		}
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
			setState(42);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,0,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(37);
				expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(38);
				preExpression();
				setState(39);
				expression();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(41);
				aggregateQuery();
				}
				break;
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
		public TerminalNode QUERY() { return getToken(PseudoQueryExpressionParser.QUERY, 0); }
		public AggregateListContext aggregateList() {
			return getRuleContext(AggregateListContext.class,0);
		}
		public TerminalNode FROM() { return getToken(PseudoQueryExpressionParser.FROM, 0); }
		public TerminalNode DATA() { return getToken(PseudoQueryExpressionParser.DATA, 0); }
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
			setState(44);
			match(QUERY);
			setState(45);
			aggregateList();
			setState(46);
			match(FROM);
			setState(47);
			match(DATA);
			setState(49);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==PAST) {
				{
				setState(48);
				timeFilter();
				}
			}

			setState(52);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==WHERE) {
				{
				setState(51);
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
		public List<TerminalNode> COMMA() { return getTokens(PseudoQueryExpressionParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(PseudoQueryExpressionParser.COMMA, i);
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
			setState(54);
			aggregate();
			setState(59);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(55);
				match(COMMA);
				setState(56);
				aggregate();
				}
				}
				setState(61);
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
		public TerminalNode LPAREN() { return getToken(PseudoQueryExpressionParser.LPAREN, 0); }
		public TerminalNode RPAREN() { return getToken(PseudoQueryExpressionParser.RPAREN, 0); }
		public TerminalNode VALUEIDENTIFIER() { return getToken(PseudoQueryExpressionParser.VALUEIDENTIFIER, 0); }
		public TerminalNode STAR() { return getToken(PseudoQueryExpressionParser.STAR, 0); }
		public AggregateContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_aggregate; }
	}

	public final AggregateContext aggregate() throws RecognitionException {
		AggregateContext _localctx = new AggregateContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_aggregate);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(62);
			aggregateFunction();
			setState(63);
			match(LPAREN);
			setState(64);
			_la = _input.LA(1);
			if ( !(_la==STAR || _la==VALUEIDENTIFIER) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			setState(65);
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
		public TerminalNode COUNT() { return getToken(PseudoQueryExpressionParser.COUNT, 0); }
		public TerminalNode SUM() { return getToken(PseudoQueryExpressionParser.SUM, 0); }
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
			setState(67);
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
		public TerminalNode PAST() { return getToken(PseudoQueryExpressionParser.PAST, 0); }
		public TerminalNode INT() { return getToken(PseudoQueryExpressionParser.INT, 0); }
		public TerminalNode TIMEUNIT() { return getToken(PseudoQueryExpressionParser.TIMEUNIT, 0); }
		public TerminalNode FROM() { return getToken(PseudoQueryExpressionParser.FROM, 0); }
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
			setState(69);
			match(PAST);
			setState(70);
			match(INT);
			setState(71);
			match(TIMEUNIT);
			setState(72);
			match(FROM);
			setState(73);
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
		public TerminalNode TRANSACTION_DATE() { return getToken(PseudoQueryExpressionParser.TRANSACTION_DATE, 0); }
		public TerminalNode INT() { return getToken(PseudoQueryExpressionParser.INT, 0); }
		public TerminalNode TIMEUNIT() { return getToken(PseudoQueryExpressionParser.TIMEUNIT, 0); }
		public TerminalNode BEFORE() { return getToken(PseudoQueryExpressionParser.BEFORE, 0); }
		public TimeReferenceContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_timeReference; }
	}

	public final TimeReferenceContext timeReference() throws RecognitionException {
		TimeReferenceContext _localctx = new TimeReferenceContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_timeReference);
		try {
			setState(80);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case TRANSACTION_DATE:
				enterOuterAlt(_localctx, 1);
				{
				setState(75);
				match(TRANSACTION_DATE);
				}
				break;
			case INT:
				enterOuterAlt(_localctx, 2);
				{
				setState(76);
				match(INT);
				setState(77);
				match(TIMEUNIT);
				setState(78);
				match(BEFORE);
				setState(79);
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
		public TerminalNode WHERE() { return getToken(PseudoQueryExpressionParser.WHERE, 0); }
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
			setState(82);
			match(WHERE);
			setState(84);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,5,_ctx) ) {
			case 1:
				{
				setState(83);
				preExpression();
				}
				break;
			}
			setState(86);
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
		public TerminalNode IDENTIFIER() { return getToken(PseudoQueryExpressionParser.IDENTIFIER, 0); }
		public TerminalNode IS() { return getToken(PseudoQueryExpressionParser.IS, 0); }
		public SourceDestSpecifierContext sourceDestSpecifier() {
			return getRuleContext(SourceDestSpecifierContext.class,0);
		}
		public TerminalNode AND() { return getToken(PseudoQueryExpressionParser.AND, 0); }
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
			setState(88);
			match(IDENTIFIER);
			setState(89);
			match(IS);
			setState(90);
			sourceDestSpecifier();
			setState(91);
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
		public TerminalNode SOURCE_TOKEN() { return getToken(PseudoQueryExpressionParser.SOURCE_TOKEN, 0); }
		public TerminalNode DEST_TOKEN() { return getToken(PseudoQueryExpressionParser.DEST_TOKEN, 0); }
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
			setState(93);
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
			setState(95);
			term();
			setState(101);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==AND || _la==OR) {
				{
				{
				setState(96);
				logicalOperator();
				setState(97);
				term();
				}
				}
				setState(103);
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
		public TerminalNode LPAREN() { return getToken(PseudoQueryExpressionParser.LPAREN, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode RPAREN() { return getToken(PseudoQueryExpressionParser.RPAREN, 0); }
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
			setState(112);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LPAREN:
				enterOuterAlt(_localctx, 1);
				{
				setState(104);
				match(LPAREN);
				setState(105);
				expression();
				setState(106);
				match(RPAREN);
				}
				break;
			case IDENTIFIER:
				enterOuterAlt(_localctx, 2);
				{
				setState(108);
				field();
				setState(109);
				operator();
				setState(110);
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
		public TerminalNode IDENTIFIER() { return getToken(PseudoQueryExpressionParser.IDENTIFIER, 0); }
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
			setState(114);
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
		public TerminalNode EQ() { return getToken(PseudoQueryExpressionParser.EQ, 0); }
		public TerminalNode NE() { return getToken(PseudoQueryExpressionParser.NE, 0); }
		public TerminalNode GT() { return getToken(PseudoQueryExpressionParser.GT, 0); }
		public TerminalNode LT() { return getToken(PseudoQueryExpressionParser.LT, 0); }
		public TerminalNode GE() { return getToken(PseudoQueryExpressionParser.GE, 0); }
		public TerminalNode LE() { return getToken(PseudoQueryExpressionParser.LE, 0); }
		public TerminalNode IN() { return getToken(PseudoQueryExpressionParser.IN, 0); }
		public TerminalNode NOT_IN() { return getToken(PseudoQueryExpressionParser.NOT_IN, 0); }
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
			setState(116);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 8269824L) != 0)) ) {
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
		public TerminalNode VALUEIDENTIFIER() { return getToken(PseudoQueryExpressionParser.VALUEIDENTIFIER, 0); }
		public TerminalNode DATE() { return getToken(PseudoQueryExpressionParser.DATE, 0); }
		public TerminalNode EMPTY() { return getToken(PseudoQueryExpressionParser.EMPTY, 0); }
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
			setState(118);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 3489660928L) != 0)) ) {
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
		public TerminalNode AND() { return getToken(PseudoQueryExpressionParser.AND, 0); }
		public TerminalNode OR() { return getToken(PseudoQueryExpressionParser.OR, 0); }
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
			setState(120);
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
		"\u0004\u0001\"{\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001\u0002"+
		"\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004\u0007\u0004\u0002"+
		"\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007\u0007\u0007\u0002"+
		"\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002\u000b\u0007\u000b\u0002"+
		"\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e\u0002\u000f\u0007\u000f"+
		"\u0002\u0010\u0007\u0010\u0001\u0000\u0001\u0000\u0001\u0000\u0001\u0001"+
		"\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0003\u0001+\b\u0001"+
		"\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0003\u0002"+
		"2\b\u0002\u0001\u0002\u0003\u00025\b\u0002\u0001\u0003\u0001\u0003\u0001"+
		"\u0003\u0005\u0003:\b\u0003\n\u0003\f\u0003=\t\u0003\u0001\u0004\u0001"+
		"\u0004\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0005\u0001\u0005\u0001"+
		"\u0006\u0001\u0006\u0001\u0006\u0001\u0006\u0001\u0006\u0001\u0006\u0001"+
		"\u0007\u0001\u0007\u0001\u0007\u0001\u0007\u0001\u0007\u0003\u0007Q\b"+
		"\u0007\u0001\b\u0001\b\u0003\bU\b\b\u0001\b\u0001\b\u0001\t\u0001\t\u0001"+
		"\t\u0001\t\u0001\t\u0001\n\u0001\n\u0001\u000b\u0001\u000b\u0001\u000b"+
		"\u0001\u000b\u0005\u000bd\b\u000b\n\u000b\f\u000bg\t\u000b\u0001\f\u0001"+
		"\f\u0001\f\u0001\f\u0001\f\u0001\f\u0001\f\u0001\f\u0003\fq\b\f\u0001"+
		"\r\u0001\r\u0001\u000e\u0001\u000e\u0001\u000f\u0001\u000f\u0001\u0010"+
		"\u0001\u0010\u0001\u0010\u0000\u0000\u0011\u0000\u0002\u0004\u0006\b\n"+
		"\f\u000e\u0010\u0012\u0014\u0016\u0018\u001a\u001c\u001e \u0000\u0006"+
		"\u0002\u0000\u001a\u001a\u001e\u001e\u0001\u0000\n\u000b\u0001\u0000\u000f"+
		"\u0010\u0002\u0000\f\r\u0011\u0016\u0002\u0000\u001c\u001c\u001e\u001f"+
		"\u0001\u0000\u0005\u0006r\u0000\"\u0001\u0000\u0000\u0000\u0002*\u0001"+
		"\u0000\u0000\u0000\u0004,\u0001\u0000\u0000\u0000\u00066\u0001\u0000\u0000"+
		"\u0000\b>\u0001\u0000\u0000\u0000\nC\u0001\u0000\u0000\u0000\fE\u0001"+
		"\u0000\u0000\u0000\u000eP\u0001\u0000\u0000\u0000\u0010R\u0001\u0000\u0000"+
		"\u0000\u0012X\u0001\u0000\u0000\u0000\u0014]\u0001\u0000\u0000\u0000\u0016"+
		"_\u0001\u0000\u0000\u0000\u0018p\u0001\u0000\u0000\u0000\u001ar\u0001"+
		"\u0000\u0000\u0000\u001ct\u0001\u0000\u0000\u0000\u001ev\u0001\u0000\u0000"+
		"\u0000 x\u0001\u0000\u0000\u0000\"#\u0003\u0002\u0001\u0000#$\u0005\u0000"+
		"\u0000\u0001$\u0001\u0001\u0000\u0000\u0000%+\u0003\u0016\u000b\u0000"+
		"&\'\u0003\u0012\t\u0000\'(\u0003\u0016\u000b\u0000(+\u0001\u0000\u0000"+
		"\u0000)+\u0003\u0004\u0002\u0000*%\u0001\u0000\u0000\u0000*&\u0001\u0000"+
		"\u0000\u0000*)\u0001\u0000\u0000\u0000+\u0003\u0001\u0000\u0000\u0000"+
		",-\u0005\u0001\u0000\u0000-.\u0003\u0006\u0003\u0000./\u0005\u0002\u0000"+
		"\u0000/1\u0005\u0003\u0000\u000002\u0003\f\u0006\u000010\u0001\u0000\u0000"+
		"\u000012\u0001\u0000\u0000\u000024\u0001\u0000\u0000\u000035\u0003\u0010"+
		"\b\u000043\u0001\u0000\u0000\u000045\u0001\u0000\u0000\u00005\u0005\u0001"+
		"\u0000\u0000\u00006;\u0003\b\u0004\u000078\u0005\u0019\u0000\u00008:\u0003"+
		"\b\u0004\u000097\u0001\u0000\u0000\u0000:=\u0001\u0000\u0000\u0000;9\u0001"+
		"\u0000\u0000\u0000;<\u0001\u0000\u0000\u0000<\u0007\u0001\u0000\u0000"+
		"\u0000=;\u0001\u0000\u0000\u0000>?\u0003\n\u0005\u0000?@\u0005\u0017\u0000"+
		"\u0000@A\u0007\u0000\u0000\u0000AB\u0005\u0018\u0000\u0000B\t\u0001\u0000"+
		"\u0000\u0000CD\u0007\u0001\u0000\u0000D\u000b\u0001\u0000\u0000\u0000"+
		"EF\u0005\b\u0000\u0000FG\u0005 \u0000\u0000GH\u0005\u001b\u0000\u0000"+
		"HI\u0005\u0002\u0000\u0000IJ\u0003\u000e\u0007\u0000J\r\u0001\u0000\u0000"+
		"\u0000KQ\u0005\u000e\u0000\u0000LM\u0005 \u0000\u0000MN\u0005\u001b\u0000"+
		"\u0000NO\u0005\t\u0000\u0000OQ\u0005\u000e\u0000\u0000PK\u0001\u0000\u0000"+
		"\u0000PL\u0001\u0000\u0000\u0000Q\u000f\u0001\u0000\u0000\u0000RT\u0005"+
		"\u0004\u0000\u0000SU\u0003\u0012\t\u0000TS\u0001\u0000\u0000\u0000TU\u0001"+
		"\u0000\u0000\u0000UV\u0001\u0000\u0000\u0000VW\u0003\u0016\u000b\u0000"+
		"W\u0011\u0001\u0000\u0000\u0000XY\u0005\u001d\u0000\u0000YZ\u0005\u0007"+
		"\u0000\u0000Z[\u0003\u0014\n\u0000[\\\u0005\u0005\u0000\u0000\\\u0013"+
		"\u0001\u0000\u0000\u0000]^\u0007\u0002\u0000\u0000^\u0015\u0001\u0000"+
		"\u0000\u0000_e\u0003\u0018\f\u0000`a\u0003 \u0010\u0000ab\u0003\u0018"+
		"\f\u0000bd\u0001\u0000\u0000\u0000c`\u0001\u0000\u0000\u0000dg\u0001\u0000"+
		"\u0000\u0000ec\u0001\u0000\u0000\u0000ef\u0001\u0000\u0000\u0000f\u0017"+
		"\u0001\u0000\u0000\u0000ge\u0001\u0000\u0000\u0000hi\u0005\u0017\u0000"+
		"\u0000ij\u0003\u0016\u000b\u0000jk\u0005\u0018\u0000\u0000kq\u0001\u0000"+
		"\u0000\u0000lm\u0003\u001a\r\u0000mn\u0003\u001c\u000e\u0000no\u0003\u001e"+
		"\u000f\u0000oq\u0001\u0000\u0000\u0000ph\u0001\u0000\u0000\u0000pl\u0001"+
		"\u0000\u0000\u0000q\u0019\u0001\u0000\u0000\u0000rs\u0005\u001d\u0000"+
		"\u0000s\u001b\u0001\u0000\u0000\u0000tu\u0007\u0003\u0000\u0000u\u001d"+
		"\u0001\u0000\u0000\u0000vw\u0007\u0004\u0000\u0000w\u001f\u0001\u0000"+
		"\u0000\u0000xy\u0007\u0005\u0000\u0000y!\u0001\u0000\u0000\u0000\b*14"+
		";PTep";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}