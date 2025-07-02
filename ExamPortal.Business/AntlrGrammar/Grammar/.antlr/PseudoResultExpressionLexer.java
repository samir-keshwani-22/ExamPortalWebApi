// Generated from d:/ExamPortalWebApi/ExamPortal.Business/AntlrGrammar/Grammar/PseudoResultExpression.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue", "this-escape"})
public class PseudoResultExpressionLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, IDENTIFIER=16, 
		NUMBER=17, WS=18, INVALID=19;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
			"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "IDENTIFIER", "NUMBER", 
			"WS", "INVALID"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'('", "')'", "'abs('", "'-'", "'+'", "'='", "'!='", "'>'", "'<'", 
			"'>='", "'<='", "'AND'", "'OR'", "'/'", "'*'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, "IDENTIFIER", "NUMBER", "WS", "INVALID"
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


	public PseudoResultExpressionLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "PseudoResultExpression.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\u0004\u0000\u0013u\u0006\uffff\uffff\u0002\u0000\u0007\u0000\u0002\u0001"+
		"\u0007\u0001\u0002\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004"+
		"\u0007\u0004\u0002\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007"+
		"\u0007\u0007\u0002\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002\u000b"+
		"\u0007\u000b\u0002\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e\u0002"+
		"\u000f\u0007\u000f\u0002\u0010\u0007\u0010\u0002\u0011\u0007\u0011\u0002"+
		"\u0012\u0007\u0012\u0001\u0000\u0001\u0000\u0001\u0001\u0001\u0001\u0001"+
		"\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0003\u0001"+
		"\u0003\u0001\u0004\u0001\u0004\u0001\u0005\u0001\u0005\u0001\u0006\u0001"+
		"\u0006\u0001\u0006\u0001\u0007\u0001\u0007\u0001\b\u0001\b\u0001\t\u0001"+
		"\t\u0001\t\u0001\n\u0001\n\u0001\n\u0001\u000b\u0001\u000b\u0001\u000b"+
		"\u0001\u000b\u0001\f\u0001\f\u0001\f\u0001\r\u0001\r\u0001\u000e\u0001"+
		"\u000e\u0001\u000f\u0001\u000f\u0004\u000fQ\b\u000f\u000b\u000f\f\u000f"+
		"R\u0001\u000f\u0001\u000f\u0001\u000f\u0001\u000f\u0001\u000f\u0001\u000f"+
		"\u0001\u000f\u0001\u000f\u0001\u000f\u0003\u000f^\b\u000f\u0001\u0010"+
		"\u0004\u0010a\b\u0010\u000b\u0010\f\u0010b\u0001\u0010\u0001\u0010\u0004"+
		"\u0010g\b\u0010\u000b\u0010\f\u0010h\u0003\u0010k\b\u0010\u0001\u0011"+
		"\u0004\u0011n\b\u0011\u000b\u0011\f\u0011o\u0001\u0011\u0001\u0011\u0001"+
		"\u0012\u0001\u0012\u0000\u0000\u0013\u0001\u0001\u0003\u0002\u0005\u0003"+
		"\u0007\u0004\t\u0005\u000b\u0006\r\u0007\u000f\b\u0011\t\u0013\n\u0015"+
		"\u000b\u0017\f\u0019\r\u001b\u000e\u001d\u000f\u001f\u0010!\u0011#\u0012"+
		"%\u0013\u0001\u0000\u0002\u0001\u000009\u0003\u0000\t\n\r\r  z\u0000\u0001"+
		"\u0001\u0000\u0000\u0000\u0000\u0003\u0001\u0000\u0000\u0000\u0000\u0005"+
		"\u0001\u0000\u0000\u0000\u0000\u0007\u0001\u0000\u0000\u0000\u0000\t\u0001"+
		"\u0000\u0000\u0000\u0000\u000b\u0001\u0000\u0000\u0000\u0000\r\u0001\u0000"+
		"\u0000\u0000\u0000\u000f\u0001\u0000\u0000\u0000\u0000\u0011\u0001\u0000"+
		"\u0000\u0000\u0000\u0013\u0001\u0000\u0000\u0000\u0000\u0015\u0001\u0000"+
		"\u0000\u0000\u0000\u0017\u0001\u0000\u0000\u0000\u0000\u0019\u0001\u0000"+
		"\u0000\u0000\u0000\u001b\u0001\u0000\u0000\u0000\u0000\u001d\u0001\u0000"+
		"\u0000\u0000\u0000\u001f\u0001\u0000\u0000\u0000\u0000!\u0001\u0000\u0000"+
		"\u0000\u0000#\u0001\u0000\u0000\u0000\u0000%\u0001\u0000\u0000\u0000\u0001"+
		"\'\u0001\u0000\u0000\u0000\u0003)\u0001\u0000\u0000\u0000\u0005+\u0001"+
		"\u0000\u0000\u0000\u00070\u0001\u0000\u0000\u0000\t2\u0001\u0000\u0000"+
		"\u0000\u000b4\u0001\u0000\u0000\u0000\r6\u0001\u0000\u0000\u0000\u000f"+
		"9\u0001\u0000\u0000\u0000\u0011;\u0001\u0000\u0000\u0000\u0013=\u0001"+
		"\u0000\u0000\u0000\u0015@\u0001\u0000\u0000\u0000\u0017C\u0001\u0000\u0000"+
		"\u0000\u0019G\u0001\u0000\u0000\u0000\u001bJ\u0001\u0000\u0000\u0000\u001d"+
		"L\u0001\u0000\u0000\u0000\u001fN\u0001\u0000\u0000\u0000!`\u0001\u0000"+
		"\u0000\u0000#m\u0001\u0000\u0000\u0000%s\u0001\u0000\u0000\u0000\'(\u0005"+
		"(\u0000\u0000(\u0002\u0001\u0000\u0000\u0000)*\u0005)\u0000\u0000*\u0004"+
		"\u0001\u0000\u0000\u0000+,\u0005a\u0000\u0000,-\u0005b\u0000\u0000-.\u0005"+
		"s\u0000\u0000./\u0005(\u0000\u0000/\u0006\u0001\u0000\u0000\u000001\u0005"+
		"-\u0000\u00001\b\u0001\u0000\u0000\u000023\u0005+\u0000\u00003\n\u0001"+
		"\u0000\u0000\u000045\u0005=\u0000\u00005\f\u0001\u0000\u0000\u000067\u0005"+
		"!\u0000\u000078\u0005=\u0000\u00008\u000e\u0001\u0000\u0000\u00009:\u0005"+
		">\u0000\u0000:\u0010\u0001\u0000\u0000\u0000;<\u0005<\u0000\u0000<\u0012"+
		"\u0001\u0000\u0000\u0000=>\u0005>\u0000\u0000>?\u0005=\u0000\u0000?\u0014"+
		"\u0001\u0000\u0000\u0000@A\u0005<\u0000\u0000AB\u0005=\u0000\u0000B\u0016"+
		"\u0001\u0000\u0000\u0000CD\u0005A\u0000\u0000DE\u0005N\u0000\u0000EF\u0005"+
		"D\u0000\u0000F\u0018\u0001\u0000\u0000\u0000GH\u0005O\u0000\u0000HI\u0005"+
		"R\u0000\u0000I\u001a\u0001\u0000\u0000\u0000JK\u0005/\u0000\u0000K\u001c"+
		"\u0001\u0000\u0000\u0000LM\u0005*\u0000\u0000M\u001e\u0001\u0000\u0000"+
		"\u0000NP\u0005Q\u0000\u0000OQ\u0007\u0000\u0000\u0000PO\u0001\u0000\u0000"+
		"\u0000QR\u0001\u0000\u0000\u0000RP\u0001\u0000\u0000\u0000RS\u0001\u0000"+
		"\u0000\u0000ST\u0001\u0000\u0000\u0000T]\u0005_\u0000\u0000UV\u0005c\u0000"+
		"\u0000VW\u0005o\u0000\u0000WX\u0005u\u0000\u0000XY\u0005n\u0000\u0000"+
		"Y^\u0005t\u0000\u0000Z[\u0005s\u0000\u0000[\\\u0005u\u0000\u0000\\^\u0005"+
		"m\u0000\u0000]U\u0001\u0000\u0000\u0000]Z\u0001\u0000\u0000\u0000^ \u0001"+
		"\u0000\u0000\u0000_a\u0007\u0000\u0000\u0000`_\u0001\u0000\u0000\u0000"+
		"ab\u0001\u0000\u0000\u0000b`\u0001\u0000\u0000\u0000bc\u0001\u0000\u0000"+
		"\u0000cj\u0001\u0000\u0000\u0000df\u0005.\u0000\u0000eg\u0007\u0000\u0000"+
		"\u0000fe\u0001\u0000\u0000\u0000gh\u0001\u0000\u0000\u0000hf\u0001\u0000"+
		"\u0000\u0000hi\u0001\u0000\u0000\u0000ik\u0001\u0000\u0000\u0000jd\u0001"+
		"\u0000\u0000\u0000jk\u0001\u0000\u0000\u0000k\"\u0001\u0000\u0000\u0000"+
		"ln\u0007\u0001\u0000\u0000ml\u0001\u0000\u0000\u0000no\u0001\u0000\u0000"+
		"\u0000om\u0001\u0000\u0000\u0000op\u0001\u0000\u0000\u0000pq\u0001\u0000"+
		"\u0000\u0000qr\u0006\u0011\u0000\u0000r$\u0001\u0000\u0000\u0000st\t\u0000"+
		"\u0000\u0000t&\u0001\u0000\u0000\u0000\u0007\u0000R]bhjo\u0001\u0006\u0000"+
		"\u0000";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}